using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private BossEntity boss;

    void Update()
    {
        if (boss != null && bar != null)
        {
            bar.fillAmount = (float)boss.CurrentHealth / boss.MaxHealth;
        }
    }
}