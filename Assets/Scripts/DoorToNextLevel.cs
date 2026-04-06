using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 
    [SerializeField] private KeyCode interactKey = KeyCode.Return; 
    public GameObject black;

    private bool isPlayerNear = false;
    //public bool isClear = false;


    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactKey) && Scene1Manager.Instance.killCounter >= 30)
        {
            Instantiate(black);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerNear = false;
    }
}


