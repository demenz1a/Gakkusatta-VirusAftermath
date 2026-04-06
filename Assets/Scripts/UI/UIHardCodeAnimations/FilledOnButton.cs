using UnityEngine;
using UnityEngine.UI;

public class FilledOnButton : MonoBehaviour
{
    public static FilledOnButton instance;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private float attackCooldown = 3f; 

    private float cooldownTimer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        cooldownImage.fillAmount = 0;
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0f) cooldownTimer = 0f;

            cooldownImage.fillAmount = cooldownTimer / attackCooldown;
        }
    }

    public void StartCooldown()
    {
        cooldownTimer = attackCooldown;
        cooldownImage.fillAmount = 1f;
    }
}

