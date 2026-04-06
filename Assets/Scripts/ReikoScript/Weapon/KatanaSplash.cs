using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
{
    [SerializeField] private Katana katana;

    private const string ATTACK = "IsAttack";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        katana.onKatanaSwing += Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }


}