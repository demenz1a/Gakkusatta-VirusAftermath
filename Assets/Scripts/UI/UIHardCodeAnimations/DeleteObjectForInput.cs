using UnityEngine;

public class DeleteObjectForInput : MonoBehaviour
{
    private void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            DestroyYourself();
        }
    }

    private void DestroyYourself()
    {
        Destroy(gameObject);
    }
}
