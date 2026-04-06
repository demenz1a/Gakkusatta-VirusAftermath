using UnityEngine;

public class TriggerForZombiespawn : MonoBehaviour
{
    [SerializeField] private Animator animatorDoor1;
    [SerializeField] private Animator animatorDoor2;
    private const string ISOPENDOOR = "OpenDoor";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && SceneStorage.shouldEnableObject)
        {
            animatorDoor1.SetTrigger(ISOPENDOOR);
            animatorDoor2.SetTrigger(ISOPENDOOR);
        }
    }
}
