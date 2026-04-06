using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class XmarkVisual : MonoBehaviour
{    
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private Xmark _xmark;
    private Animator _animator;
    private const string ISPULSE = "IsPulsing";
    private const string ISACTIVE = "IsActive";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        _animator.SetBool(ISPULSE, _xmark.IsPulse());
        _animator.SetBool(ISACTIVE, _xmark.IsActive());
    }
}
