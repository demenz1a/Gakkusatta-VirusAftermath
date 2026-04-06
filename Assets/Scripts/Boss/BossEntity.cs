using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Collections;

public class BossEntity : MonoBehaviour
{
    public static BossEntity Instance { get; private set; }
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public CinemachineCamera bossCam;
    public AudioSource BossSource;
    public AudioSource deathSource;
    public DialogueManager blockDialogue;
    public GameObject bossManager;
    [SerializeField] private int _maxHealth;    
    [SerializeField] private GameObject black;
    [SerializeField] private AudioClip death;
    private int _currentHealth;
    private bool _isDead;
    private PolygonCollider2D _polygonCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private const string ISDEAD = "IsDead";

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void TakeDamageBoss(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        DetectDeath();
    }

    public void TakeDamageBossMinus(int damage)
    {
        _currentHealth -= (-damage);
        Debug.Log(_currentHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead) return;

        _isDead = true;
        audioSource.Stop();
        bossManager.SetActive(false);
        BossSource.enabled = false;
        StartCoroutine(ShowDeadDialogue());
        animator.SetBool(ISDEAD, true);
        bossCam.Priority = 20;
        StartCoroutine(LoadWinnerWorldAfterDelay(4f));
    }

    private IEnumerator LoadWinnerWorldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("WinnerWorld");
    }

    private IEnumerator ShowDeadDialogue()
    {

        blockDialogue.gameObject.SetActive(true);
        blockDialogue.StartDialogueFromIndex(21);

        yield return new WaitUntil(() => !blockDialogue.gameObject.activeSelf);
    }

    private IEnumerator ShowPastDeadDialogue()
    {

        blockDialogue.gameObject.SetActive(true);

        yield return new WaitUntil(() => !blockDialogue.gameObject.activeSelf);
    }

    private void DeathSound()
    {
        deathSource.PlayOneShot(death);
    }

    private void SpawnBlack()
    {
        Instantiate(black);
    }
}
