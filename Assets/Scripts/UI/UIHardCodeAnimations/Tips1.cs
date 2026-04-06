using UnityEngine;

public class Tips1 : MonoBehaviour
{
    [SerializeField] private CounterManager counterManager;
    private Animator _animator;
    private const string TIPS = "Tips1";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counterManager.OnTips1 += _counter_OnTips1;
    }

    private void _counter_OnTips1(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TIPS);
    }
}
