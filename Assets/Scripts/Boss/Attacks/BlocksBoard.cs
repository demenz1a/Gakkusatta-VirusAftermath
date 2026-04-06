using UnityEngine;
using TMPro;

public class BlocksBoard : MonoBehaviour
{
    public TextMeshProUGUI expressionText;
    public void SetExpression(string expr)
    {
        expressionText.text = expr;
    }
}
