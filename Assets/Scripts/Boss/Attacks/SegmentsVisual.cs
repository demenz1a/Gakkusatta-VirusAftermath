using UnityEngine;

public class SegmentsVisual : MonoBehaviour
{    
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private Segments _segments;
    private Animator _animator;
    private const string ISATTACK = "IsAttack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _animator.SetBool(ISATTACK, _segments.IsAttack());
    }
}
