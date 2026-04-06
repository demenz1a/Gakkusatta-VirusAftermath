using UnityEngine;
using TMPro; // обязательно для TextMeshPro

public class CounterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText; // ссылка на объект с TMP-текстом
    [SerializeField] private int value = 10; // начальное значение

    private void Start()
    {
        UpdateText();
    }

    // Метод, который можно повесить на кнопку
    public void DecreaseValue()
    {
        value--;
        UpdateText();
    }

    private void UpdateText()
    {
        counterText.text = value.ToString();
    }
}

