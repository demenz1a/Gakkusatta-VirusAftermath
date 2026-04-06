using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;
using System.Collections.Generic;

public class Reiko : MonoBehaviour
{
    public bool IsDead { get; private set; }
    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] public int maxHealth;
    public static Reiko Instance { get; private set; }
    public static Reiko _instance;
    public int _currentHealth;
    public CinemachineCamera playerCam;
    public AudioSource audioSource;
    public AudioSource deathSource;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDead;
    [SerializeField] private float _dogdeSpeed = 1000f;
    [SerializeField] private float _dogdeDuration = 0.2f;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip ghost;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip ultimateClip;
    [SerializeField] private GameObject bossManager;
    [SerializeField] private GameObject black;
    [SerializeField] private PolygonCollider2D PolygonCollider2D;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject slash;
    [SerializeField] private GameObject ultimate;
    [SerializeField] private GameObject customCursor;
    [SerializeField] private FilledOnButton filledOnButton1;
    [SerializeField] private FilledOnButton filledOnButton2;
    private Rigidbody2D _rigidbody;
    private KnockBack _knockBack;
    private bool _isRuning = false;
    private bool _isDash = false;
    public bool _isDead = false;
    private float _dogdeTimer = 0f;
    private Vector2 _dogdeDir;
    private ReikoVisual _reikoVisual;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;
    private Vector2 _lastMoveDir = Vector2.right;
    public float _dogdeCooldown = 3f;      
    private float _dogdeCooldownTimer = 0f;
    public float _UltimateCooldown = 8f;
    private float _UltimateCooldownTimer = 0f;
    public DialogueManager dialogueManager;
    [SerializeField] private float dashHitRadius = 0.8f;
    [SerializeField] private KnockbackFeedback knockbackFeedback;
    [SerializeField] private string defaultLayerName = "Default";
    [SerializeField] private string dashLayerName = "Ignore Enemy";

    [Header("Ульта")]
    public GameObject ultEffectPrefab;   
    public float ultDuration = 2f;      

    [Header("Слэши после ульты")]
    public GameObject slashPrefab;      
    public int slashCount = 8;           
    public float spawnRadius = 3f;      
    public float spawnDelay = 0.2f;     

    private bool isUltActive = false;

    private int _defaultLayer;
    private int _dashLayer;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();

        Instance = this;

        _defaultLayer = LayerMask.NameToLayer(defaultLayerName);
        _dashLayer = LayerMask.NameToLayer(dashLayerName);

    }

    private void Start()
    {
        if (Scene1Manager.Instance.isFirstStart)
        {
            Scene1Manager.Instance.ReikoHP = maxHealth;
            Scene1Manager.Instance.isFirstStart = false;
        }
        ReikoInputs.Instance.OnReikoAttack += ReikoInputs_OnReikoAttack;
        ReikoInputs.Instance.OnReikoDogde += ReikoInputs_OnReikoDogde;
        ReikoInputs.Instance.OnReikoUltimate += ReikoInputs_ReikoUltimate;
        _reikoVisual = FindObjectOfType<ReikoVisual>();

        audioSource.playOnAwake = false;
    }

    public void StartInvincibilityInAnimation()
    {
        isInvincible = true;
    }

    public void EndInvincibilityInAnimation()
    {
        isInvincible = false;
    }

    private void ReikoInputs_OnReikoAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void ReikoInputs_OnReikoDogde(object sender, System.EventArgs e)
    {
        if (_dogdeCooldownTimer > 0f || _isDash)
            return;

        _dogdeDir = _lastMoveDir;
        _isDash = true;
        _dogdeTimer = _dogdeDuration;
        _dogdeCooldownTimer = _dogdeCooldown;

    }

    private void ReikoInputs_ReikoUltimate(object sender, System.EventArgs e)
    {
        if (_UltimateCooldownTimer > 0f)
            return;

        ActivateUlt();
        PlayUlt();
        _UltimateCooldownTimer = _UltimateCooldown;
    }

    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;

        if (_isDead) return;

        _dogdeCooldownTimer -= Time.fixedDeltaTime;
        _UltimateCooldownTimer -= Time.fixedDeltaTime;

        Cursor.visible = true;
        customCursor.SetActive(false);
        Movement();
        Dash();
        UpdateInvincibility();
        DetectDeath();
    }

    public void TakeDamageWithKB(GameObject damageSource, int damage)
    {
        if (isInvincible) return;

        Scene1Manager.Instance.ReikoHP = Mathf.Max(0, Scene1Manager.Instance.ReikoHP - damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        Debug.Log(Scene1Manager.Instance.ReikoHP);
        if (knockbackFeedback != null)
            knockbackFeedback.PlayFeedback(damageSource);
        StartInvincibility();
        DetectDeath();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)

        {

            //StartCoroutine(SpeedBoost());
            return;
        }

        Scene1Manager.Instance.ReikoHP = Mathf.Max(0, Scene1Manager.Instance.ReikoHP - damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        audioSource.PlayOneShot(hit);
        Debug.Log(Scene1Manager.Instance.ReikoHP);
        StartInvincibility();
        DetectDeath();
        ScreenShaker.Instance.Shake();
        //katana.AttackColliderTurnoff();
    }

    private IEnumerator SpeedBoost()
    {
        movingSpeed += 100;
        yield return new WaitForSeconds(0.5f);
        movingSpeed -= 100;
    }
    private void UpdateInvincibility()
    {
        if (_isDash) return;

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
        if (_isDash) return;
        if (_isDead) return;


        Vector2 inputVector = ReikoInputs.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        _rigidbody.MovePosition(_rigidbody.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (inputVector.sqrMagnitude > 0.01f)
        {
            _lastMoveDir = inputVector;
            _isRuning = true;
            _reikoVisual.AdjustPlayerFacingDirection(inputVector);
        }
        else
        {
            _isRuning = false;
        }
    }

    private Coroutine _dashTrailCoroutine;

    private void Dash()
    {
        if (dialogueManager != null && dialogueManager.isDialogue) return;

        if (_isDash)
        {
            _dogdeTimer -= Time.fixedDeltaTime;

            if (_dogdeTimer <= 0f)
            {
                _isDash = false;
                gameObject.layer = _defaultLayer; 

                if (_dashTrailCoroutine != null)
                {
                    StopCoroutine(_dashTrailCoroutine);
                    _dashTrailCoroutine = null;
                }
                return;
            }

            if (_dashTrailCoroutine == null)
            {
                gameObject.layer = _dashLayer; 
                _dashTrailCoroutine = StartCoroutine(SpawnSlashTrail());
            }

            _rigidbody.MovePosition(_rigidbody.position + _dogdeDir * (_dogdeSpeed * Time.fixedDeltaTime));
            _reikoVisual.AdjustPlayerFacingDirection(_dogdeDir);

            filledOnButton1.StartCooldown();
        }
    }


    private IEnumerator SpawnSlashTrail()
    {
        while (_isDash)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 7f);
            float randomAngle = UnityEngine.Random.Range(105f, 80f); 
            Quaternion rotation = Quaternion.Euler(0, 0, randomAngle); 
            Instantiate(slash, spawnPos, rotation); 
            yield return new WaitForSeconds(0.1f); 
        } 
    }

    public void ActivateUlt()
    {
        if (isUltActive) return;
        StartCoroutine(UltSequence());
        filledOnButton2.StartCooldown();
    }

    private IEnumerator UltSequence()
    {
        isUltActive = true;

        GameObject ult = Instantiate(ultEffectPrefab, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(0.8f);

        Destroy(ult);

        for (int i = 0; i < slashCount; i++)
        {
            float angle = i * Mathf.PI * 2f / slashCount; 
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * spawnRadius;
            Vector3 spawnPos = transform.position + offset;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -offset.normalized);

            GameObject slash = Instantiate(slashPrefab, spawnPos, rotation);

            float randomScale = UnityEngine.Random.Range(0.8f, 1.5f);
            slash.transform.localScale *= randomScale;

            slash.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));

            yield return new WaitForSeconds(spawnDelay);
        }

        isUltActive = false;
    }


    private void DetectDeath()
    {
        if (Scene1Manager.Instance.ReikoHP <= 0 && !_isDead)
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
        if (Minori.Instance == null || Minori.Instance._isDead)
        {
            StartCoroutine(LoadWinnerWorldAfterDelay(0.5f));
            Debug.Log("killed");
        }
        else if (!Minori.Instance._isDead)
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

    public bool IsDeadd()
    {
        return _isDead;
    }

    public bool IsRunning()
    {
        return _isRuning;
    }

    public bool IsDogde()
    {
        return _isDash;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void PlayKatana()
    {
        audioSource.PlayOneShot(attackClip);
    }

    public void PlayDash()
    {
        audioSource.PlayOneShot(dash);
    }

    private void PlayGhost()
    {
        audioSource.PlayOneShot(ghost);
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

