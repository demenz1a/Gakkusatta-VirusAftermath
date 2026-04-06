using System.Threading;
using UnityEngine;

public class BlackSpawn : MonoBehaviour
{
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject start;

    private GameObject spawnedBlack;
    private float _timer = 8f;
    private float _delay;
    private bool _isAnim = false;

    private Animator animator;
    public SpriteRenderer spriteRenderer;

    private const string ISANIM = "IsAnim";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _delay = _timer;
    }
    private void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay < 0)
        {
            _isAnim = true;
            animator.SetBool(ISANIM, isAnim());
        }
     }


    public void SpawnBlack()
    {
        spawnedBlack = Instantiate(black);
        Instantiate(start);
    }

    public void DestroyBlack()
    {
        Destroy(spawnedBlack);
    }

    private bool isAnim()
    {
        return _isAnim;
    }

}
