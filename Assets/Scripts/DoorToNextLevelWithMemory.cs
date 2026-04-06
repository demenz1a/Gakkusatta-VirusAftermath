using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevelWithMemory : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 
    [SerializeField] private string returnSpawnID = "ReturnFromLevel2"; 
    [SerializeField] private KeyCode interactKey = KeyCode.Return; 
    [SerializeField] private Katana katana;
    public static DoorToNextLevelWithMemory instance;

    private bool isPlayerNear = false;
    public bool isGacha = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactKey))
        {
            SceneStorage.lastScene = SceneManager.GetActiveScene().name;

            Scene1Manager.Instance.returnSpawnID = returnSpawnID;

            SceneManager.LoadScene(sceneToLoad);
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

