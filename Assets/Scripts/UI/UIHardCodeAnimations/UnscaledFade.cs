using UnityEngine;

public class UnscaledFade : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    public void StartDialogue()
    {
        dialogueManager.StartDialogueFromIndex(0);
    }
}
