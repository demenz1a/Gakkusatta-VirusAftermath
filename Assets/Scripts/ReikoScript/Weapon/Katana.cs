using System;
using UnityEngine;

public class Katana : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygonCollider2D;
    [SerializeField] private Transform _polygonCollider2Dtransform;
    [SerializeField] private ReikoVisual _reikoVisual;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] public int _damageAmount = 2;

    [SerializeField] private Transform reikotransform;

    [SerializeField] private AudioClip _attackClip;
    private AudioSource _audioSource;

    public event EventHandler onKatanaSwing;

    public DialogueManager dialogueManager;
    public bool IsFlipped => _spriteRenderer.flipX;
    public bool canControl = true;

    private void Awake()
    {
        reikotransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_reikoVisual.spriteRenderer.flipX == true)
        {
            _polygonCollider2Dtransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _polygonCollider2Dtransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Start()
    {
        AttackColliderTurnoff();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }
    public void Attack()
    {
        if (dialogueManager.isDialogue && dialogueManager != null && !canControl) return;
        {
            onKatanaSwing?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayKatana()
    {
        _audioSource.PlayOneShot(_attackClip);
    }

    public void AttackColliderTurnoff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void AttackColliderTurnon()
    {
        _polygonCollider2D.enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
           collision.GetComponent<Blocks>()?.OnHit();
        }
        if (collision.CompareTag("Figures"))
        {
            Debug.Log("Rejected");
            Transform bossTransform = GameObject.FindGameObjectWithTag("Boss")?.transform;
            collision.GetComponent<Figures>()?.OnHit(bossTransform);
        }
        if (collision.CompareTag("Boss"))
        {
            {

            }
        }
        if (collision.transform.TryGetComponent(out MobEntity mobentity))
        {
            mobentity.TakeDamageWithKB(gameObject,_damageAmount);
        }
    }
}

