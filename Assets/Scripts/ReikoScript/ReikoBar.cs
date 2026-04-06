using UnityEngine;
using UnityEngine.UI;

public class ReikoBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Reiko reiko;
    [SerializeField] private Minori minori;

    private void Update()
    {
        if (reiko != null && bar != null)
        {
            bar.fillAmount = (float)Scene1Manager.Instance.ReikoHP / reiko.maxHealth;

            if (Scene1Manager.Instance.ReikoHP < 150)
            {
                bar.color = new Color32(161, 65, 63, 255);
            }

            if (Scene1Manager.Instance.ReikoHP > 150)
            {
                bar.color = new Color32(171, 255, 172, 255);
            }
        }

        else
        {
            bar.fillAmount = (float)Scene1Manager.Instance.MinoriHP / minori.maxHealth;

            if (Scene1Manager.Instance.MinoriHP < 150)
            {
                bar.color = new Color32(161, 65, 63, 255);
            }

            if (Scene1Manager.Instance.MinoriHP > 150)
            {
                bar.color = new Color32(171, 255, 172, 255);
            }
        }
    }
}
