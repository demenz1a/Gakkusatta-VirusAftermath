using UnityEngine;

public class FiguresVisual : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private Figures _figures;
    private Animator _animator;
    private const string ISATTACK = "IsAttack";
    private const string ISEND = "IsEnd";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(ISATTACK, _figures.IsAttack());
        _animator.SetBool(ISEND, _figures.IsEnd());
    }
}
