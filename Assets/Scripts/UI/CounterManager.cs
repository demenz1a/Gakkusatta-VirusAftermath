using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class CounterManager : MonoBehaviour
{
    public event EventHandler OnTips1;
    public event EventHandler OnTips2;
    public event EventHandler OnTips3;

    public static CounterManager Instance; 

    [Header("TextMeshPro UI")]
    public TextMeshProUGUI killCounterText;

    public string killTextFormat = "Óבטעמ חמלבט: {0}";
    public int totalZombiesKilled = 0;
    [SerializeField] private MusicManager musicManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateKillCounterText();
    }
    public DialogueManager dialogueManager;

    public void AddKill()
    {
        Scene1Manager.Instance.killCounter++;
        totalZombiesKilled++;
        UpdateKillCounterText();

        if (Scene1Manager.Instance.killCounter == 1)
        {
           StartCoroutine(Show1KillDialogue());
            OnTips1?.Invoke(this, EventArgs.Empty);
        }

        if (Scene1Manager.Instance.killCounter == 2)
        {
            musicManager.PlayMusic();
            OnTips2?.Invoke(this, EventArgs.Empty);
        }

        if (Scene1Manager.Instance.killCounter == 30)
        {
           StartCoroutine(Show30KillDialogue());
            OnTips3?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetTotalKills()
    {
        return Scene1Manager.Instance.killCounter;
    }

    public void ResetKills()
    {
        Scene1Manager.Instance.killCounter = 0;
        UpdateKillCounterText();
    }

    private void UpdateKillCounterText()
    {
        if (killCounterText != null)
        {
            killCounterText.text = string.Format(killTextFormat, Scene1Manager.Instance.killCounter);
        }
    }
    private IEnumerator Show1KillDialogue()
    {

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogueFromIndex(1);

        yield return new WaitUntil(() => !dialogueManager.gameObject.activeSelf);
    }

    private IEnumerator Show30KillDialogue()
    {

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogueFromIndex(15);

        yield return new WaitUntil(() => !dialogueManager.gameObject.activeSelf);
    }
}