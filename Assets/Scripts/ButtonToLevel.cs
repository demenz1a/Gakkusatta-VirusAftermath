using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonToLevel : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    public void OnClickReturn()
    {
        if (!string.IsNullOrEmpty(SceneStorage.lastScene))
        {
            SceneManager.LoadScene(SceneStorage.lastScene);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("Нет сохранённой сцены для возврата!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        SpawnPoint[] points = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (var point in points)
        {
            if (point.spawnID == Scene1Manager.Instance.returnSpawnID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = point.transform.position;
                }
                break;
            }
        }

        dialogueManager = FindObjectOfType<DialogueManager>();

        if (Scene1Manager.Instance.returnSpawnID == "ReturnFromLevel2"
            && Scene1Manager.Instance.firstReturnFromLevel2)
        {
            Scene1Manager.Instance.firstReturnFromLevel2 = false;

            if (dialogueManager != null)
            {
                StartCoroutine(ShowMinoriDialogue());
            }
        }
    }
    private IEnumerator ShowMinoriDialogue()
    {

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogueFromIndex(4);

        yield return new WaitUntil(() => !dialogueManager.gameObject.activeSelf);
    }

}


