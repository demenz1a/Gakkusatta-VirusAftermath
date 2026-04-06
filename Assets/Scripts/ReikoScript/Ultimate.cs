using System;
using UnityEngine;
using System.Collections;

public class Ultimate : MonoBehaviour
{
    private Reiko reiko;
    private Katana katana;
    private int _damageUltimate = 50 ;
    private int normalDamage = 0; // katana._damageAmount;
    private float normalCooldown = 0f;// reiko._dogdeCooldown;

    private void Awake()
    {
        normalDamage = katana._damageAmount;
        normalCooldown = reiko._dogdeCooldown;
    }
    public void ActiveUltimate()
    {
        reiko._dogdeCooldown = 0;
        katana._damageAmount = _damageUltimate;
        katana.Attack();
        StartCoroutine(BuffAttacksTime());
    }

    private IEnumerator BuffAttacksTime()
    {
        yield return new WaitForSeconds(10f);
        reiko._dogdeCooldown = normalCooldown;
        katana._damageAmount = normalDamage;
    }
}
