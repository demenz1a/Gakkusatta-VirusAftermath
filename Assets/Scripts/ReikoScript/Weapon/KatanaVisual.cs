using UnityEngine;

public class KatanaVisual : MonoBehaviour
{
    [SerializeField] private Katana katana;
    [SerializeField] private Animator animator;
    private const string ATTACK = "IsAttack";

    private void Start()
    {
        katana.onKatanaSwing += Katana_OnKatanaSwing;
    }

    private void Katana_OnKatanaSwing(object sender,System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

}


