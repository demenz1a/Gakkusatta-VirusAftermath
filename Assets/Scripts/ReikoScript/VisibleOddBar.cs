using UnityEngine;

public class VisibleOddBar : MonoBehaviour
{
    private void Update()
    {
        if (SceneStorage.shouldEnableObject == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
