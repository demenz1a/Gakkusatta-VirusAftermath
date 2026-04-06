using UnityEngine;

public class SlashVisual : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private Slash _slash;
    [SerializeField] private GameObject gameObjectField;
    private Animator _animator;
    private const string ISATTACK = "IsAttack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _animator.SetBool(ISATTACK, _slash.IsAttack());
    }

    public void DestroyYourself()
    {
        Destroy(gameObjectField);
    }
}
