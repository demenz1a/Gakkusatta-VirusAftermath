using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;

    private void Start()
    {
        if (SceneStorage.shouldEnableObject)
        {
            objectToEnable.SetActive(true);
            SceneStorage.shouldEnableObject = false;
        }
    }
}
