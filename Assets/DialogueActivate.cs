using UnityEngine;
using System.Collections;

public class DialogueActivate : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject attackManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Show1KillDialogue());
            attackManager.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Show1KillDialogue()
    {

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogueFromIndex(0);

        yield return new WaitUntil(() => !dialogueManager.gameObject.activeSelf);
    }
}
