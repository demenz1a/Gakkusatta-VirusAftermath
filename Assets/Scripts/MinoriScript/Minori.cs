using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class Minori : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] public int maxHealth;
    public static Minori Instance { get; private set; }
    public static Minori _instance;
    public int _currentHealth;
    public CinemachineCamera playerCam;
    public AudioSource audioSource;
    public AudioSource deathSource;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDead;
    public event EventHandler OnSkill;
    [SerializeField] private float _dogdeSpeed = 1000f;
    [SerializeField] private float _dogdeDuration = 0.2f;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip ultimateClip;
    [SerializeField] private AudioClip shot;
    [SerializeField] private GameObject bossManager;
    [SerializeField] private GameObject black;
    [SerializeField] private PolygonCollider2D PolygonCollider2D;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject ultimate; 
    [SerializeField] private CustomCursor customCursor;
    [SerializeField] private GameObject customCursorObj;
    [SerializeField] private RandomShooter randomShooter;

    [SerializeField] private FilledOnButton filledOnButton1;
    [SerializeField] private FilledOnButton filledOnButton2;
    [SerializeField] private FilledOnButton filledOnButton3;
    private Rigidbody2D _rigidbody;
    private KnockBack _knockBack;
    private bool _isRuning = false;
    private bool _isSkill = false;
    public bool _isDead = false;
    private MinoriVisual _minoriVisual;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;
    private Vector2 _lastMoveDir = Vector2.right;
    public float _SkillCooldownTimer;
    public float _SkillCooldown = 3f;
    public float _ShotCooldownTimer;
    public float _ShotCooldown = 0.5f;
    public float _UltimateCooldown = 4f;
    private float _UltimateCooldownTimer = 0f;
    public DialogueManager dialogueManager;
    [SerializeField] private CursorManager cursorManager;
    private bool _isAttack = false;
    private bool _isAiming = false;

    public event EventHandler onGunShot;
    public event EventHandler onAimShot;

    [Header("Настройки ульты")]
    public GameObject ultEffectPrefab;
    public GameObject healZonePrefab; 
    public float ultDuration = 4f;    
    public Vector3 zoneOffset = new Vector3(0, 0, 2f);
    private bool isUltActive = false;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        _minoriVisual = gameObject.GetComponent<MinoriVisual>();

        Instance = this;
    }

    private void Start()
    {
        if (Scene1Manager.Instance.isFirstStart)
        {
            Scene1Manager.Instance.MinoriHP = maxHealth;
            Scene1Manager.Instance.isFirstStart = false;
        }
        MinoriInputs.Instance.OnMinoriSkill += MinoriInputs_OnMinoriSkill;
        MinoriInputs.Instance.OnMinoriAttack += StartAiming;
        MinoriInputs.Instance.OnMinoriAttackRelease += Shoot;
        MinoriInputs.Instance.OnMinoriUltimate += MinoriInputs_MinoriUltimate;

        audioSource.playOnAwake = false;
        //Cursor.visible = false;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(1)) 
        //{
        //  cursorManager.ChangeCursor();
        //}
        if (_SkillCooldownTimer > 0f)
            _SkillCooldownTimer -= Time.deltaTime;
        if (_ShotCooldownTimer > 0f)
            _ShotCooldownTimer -= Time.deltaTime;
        if (_UltimateCooldownTimer > 0f)
            _UltimateCooldownTimer -= Time.deltaTime;
    }

    private void StartAiming(object sender, EventArgs e)
    {
        _isAiming = true;
        _rigidbody.linearVelocity = Vector2.zero;
       // _minoriVisual.SetAiming(true);
        customCursor.ShowCursor();
        onAimShot?.Invoke(this, EventArgs.Empty);
        customCursorObj.SetActive(true);
    }


    private void Shoot(object sender, EventArgs e)
    {
        if (!_isAiming) return;
        if (_ShotCooldownTimer > 0f)
            return;

        onGunShot?.Invoke(this, EventArgs.Empty);
        _isAiming = false;
       // _minoriVisual.SetAiming(false);
        customCursor.HideCursor();
        Gun.Instance.GunShot();
        customCursorObj.SetActive(false);
        filledOnButton1.StartCooldown();
    }


    public void StartInvincibilityInAnimation()
    {
        isInvincible = true;
    }

    public void EndInvincibilityInAnimation()
    {
        isInvincible = false;
    }

    private void MinoriInputs_OnMinoriSkill(object sender, System.EventArgs e)
    {
        Skill();
    }

    private void MinoriInputs_MinoriUltimate(object sender, System.EventArgs e)
    {
        if (_UltimateCooldownTimer > 0f)
            return;

        PlayUlt();
        ActivateUlt();
        _UltimateCooldownTimer = _UltimateCooldown;
    }


    private void FixedUpdate()
    {
        //if (_knockBack.IsGettingKnockedBack)
        //    return;

        if (_isDead) return;

       
        if (!_isAiming)
            Movement();

        UpdateInvincibility();
        DetectDeath();
    }

    public void TakeDamageWithKB(Transform damageSource, int damage)
    {
        if (isInvincible) return;

        Scene1Manager.Instance.MinoriHP = Mathf.Max(0, Scene1Manager.Instance.MinoriHP - damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        Debug.Log(Scene1Manager.Instance.MinoriHP);
        _knockBack.GetKnockedBack(damageSource);
        StartInvincibility();
        DetectDeath();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        Scene1Manager.Instance.MinoriHP = Mathf.Max(0, Scene1Manager.Instance.MinoriHP - damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        audioSource.PlayOneShot(hit);
        Debug.Log(Scene1Manager.Instance.MinoriHP);
        StartInvincibility();
        DetectDeath();
        ScreenShaker.Instance.Shake();
    }

    private void UpdateInvincibility()
    {
        if (_isSkill) return;

        if (isInvincible)
        {
            invincibilityTimer -= Time.fixedDeltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }


    private void Movement()
    {
        //if (_isAttack) return;
        if (_isDead) return;
        if (_isSkill) return;

        Vector2 inputVector = MinoriInputs.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        _rigidbody.MovePosition(_rigidbody.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (inputVector.sqrMagnitude > 0.01f)
        {
            _lastMoveDir = inputVector;
            _isRuning = true;
            _minoriVisual.AdjustPlayerFacingDirection(inputVector);
        }
        else
        {
            _isRuning = false;
        }
    }
    public void Attack()
    {
        if (dialogueManager.isDialogue || _ShotCooldownTimer > 0f) return;
        {
            _isAttack = true;
            onGunShot?.Invoke(this, EventArgs.Empty);
            _ShotCooldownTimer = _ShotCooldown;
            filledOnButton1.StartCooldown();
            //onShotSwing?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Skill()
    {
        if (dialogueManager != null && dialogueManager.isDialogue || _SkillCooldownTimer > 0f) return;
        
            OnSkill?.Invoke(this, EventArgs.Empty);
            _isSkill = true;
            randomShooter.ShootAtRandomEnemies();
            _SkillCooldownTimer = _SkillCooldown;
            filledOnButton2.StartCooldown();
        StartCoroutine(SkillRoutine());
    }

    private IEnumerator SkillRoutine()
    {
        _isSkill = true; 
        OnSkill?.Invoke(this, EventArgs.Empty);

        randomShooter.ShootAtRandomEnemies();
        _SkillCooldownTimer = _SkillCooldown;
        filledOnButton2.StartCooldown();

        yield return new WaitForSeconds(0.1f);

        SkillEnd(); 
    }
    public void ActivateUlt()
    {
        if (isUltActive) return;
        StartCoroutine(UltSequence());
        filledOnButton3.StartCooldown();
    }

    private IEnumerator UltSequence()
    {
        isUltActive = true;

        GameObject ult = Instantiate(ultEffectPrefab);

        yield return new WaitForSeconds(0.8f);

        Destroy(ult);

        GameObject zone = Instantiate(healZonePrefab, transform.position, Quaternion.identity);

        Destroy(zone, ultDuration);

        yield return new WaitForSeconds(ultDuration);

        isUltActive = false;
    }

    private void DetectDeath()
    {
        if (Scene1Manager.Instance.MinoriHP <= 0 && !_isDead)
        {
            Die();
            _isDead = true;
        }
    }
    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
        _isDead = true;
    }

    public void DeadSwitch()
    {
        if (Reiko.Instance == null || Reiko.Instance._isDead)
        {
            StartCoroutine(LoadWinnerWorldAfterDelay(0.5f));
            Debug.Log("killed");
        }
        else if (!Reiko.Instance._isDead)
        {
            CharacterSwitch.Instance.Switch();
            CharacterSwitch.Instance.Deactivate();
        }
    }

    private IEnumerator LoadWinnerWorldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DeadWorld");
    }

    private IEnumerator LoadBlackScreen()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(black);
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public bool IsRunning()
    {
        return _isRuning;
    }

    public bool IsHeal()
    {
        return _isSkill;
    }

    public void IsAttack()
    {
        _isAttack = false;
    }

    public void SkillEnd()
    {
        _isSkill = false;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void PlayDash()
    {
        audioSource.PlayOneShot(hit);
    }

    public void PlayShot()
    {
        audioSource.PlayOneShot(shot);
    }

    private void PlayUlt()
    {
        audioSource.PlayOneShot(ultimateClip);
    }

    private void DeathSound()
    {
        deathSource.PlayOneShot(death);
    }
}

