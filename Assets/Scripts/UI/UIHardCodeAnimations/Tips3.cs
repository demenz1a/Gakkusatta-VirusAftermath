using UnityEngine;

public class Tips3 : MonoBehaviour
{
    [SerializeField] private CounterManager counterManager;
    private Animator _animator;
    private const string TIPS = "Tips3";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counterManager.OnTips3 += _counter_OnTips3;
    }

    private void _counter_OnTips3(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TIPS);
    }
}
