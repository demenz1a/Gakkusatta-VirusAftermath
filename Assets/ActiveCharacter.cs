using UnityEngine;

public class ActiveCharacter : MonoBehaviour
{
    public void ActivateCharacter()
    {
        SceneStorage.shouldEnableObject = true;
    }
}
