using UnityEngine;

public class Tips2 : MonoBehaviour
{
    [SerializeField] private CounterManager counterManager;
    private Animator _animator;
    private const string TIPS = "Tips2";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counterManager.OnTips2 += _counter_OnTips2;
    }

    private void _counter_OnTips2(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TIPS);
    }
}
