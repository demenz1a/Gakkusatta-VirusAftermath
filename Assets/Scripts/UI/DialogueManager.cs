using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public string[] lines;
    public string[] characterNames;
    public Sprite[] portraits;
    public float speedText;

    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public TextMeshProUGUI nameText;

    private int index;
    public MusicManager musicManager;

    public AudioSource voiceSource;
    public float voicePlayDelay = 0.05f; 
    private float lastVoiceTime;
    public AudioClip[] characterVoices;

    public static bool hasSeenBlockDialogue = false;
    public static bool hasSeenBlockDialogueNotBoss = false;
    public bool isDialogue = false;
    public static DialogueManager instance;

    public bool isBoss = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (hasSeenBlockDialogue)
        {
            gameObject.SetActive(false);
            return;
        }

        if (hasSeenBlockDialogueNotBoss)
        {
            gameObject.SetActive(false);
            return;
        }

        EndDialogue();

        if (!isBoss)
        {
           StartCoroutine(StartDialogueWithDelay(0.5f));
        }
         //if (isBoss)
        //{
          //  return;
        //}

        dialogueText.text = string.Empty;
        //StartDialogue();
        voiceSource.volume = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            SkipTextClick();
        }
    }

    private System.Collections.IEnumerator StartDialogueWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartDialogue();
    }

    public void StartDialogue()
    {
        StartDialogueFromIndex(0);
    }

    public void StartDialogueFromIndex(int startIndex)
    {
        Time.timeScale = 0f;
        index = startIndex;
        dialogueText.text = string.Empty;
        UpdatePortraitAndCharName();
        StartCoroutine(TypeLine());
        isDialogue = true;
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = string.Empty;
        lastVoiceTime = 0f;

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;

            if (c != ' ' && Time.unscaledTime - lastVoiceTime >= voicePlayDelay)
            {
              
                lastVoiceTime = Time.unscaledTime;
            }

            yield return new WaitForSecondsRealtime(speedText);
        }
    }


    public void SkipTextClick()
    {
        if (dialogueText.text == lines[index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }

    private void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            UpdatePortraitAndCharName();

            if (index == 15 && isBoss) 
            {
                EndDialogue();
                musicManager.PlayMusic();
                return;
            }

            if (index == 21 && isBoss)
            {
                EndDialogue();
                return;
            }

            if (index == 24 && isBoss)
            {
                EndDialogue();
                return;
            }

            if (index == 1 && !isBoss)
            {
                EndDialogue();
                return;
            }

            if (index == 3 && !isBoss)
            {
                EndDialogue();
                return;
            }

            if (index == 15 && !isBoss)
            {
                EndDialogue();
                return;
            }

            if (index == 17 && !isBoss)
            {
                EndDialogue();
                return;
            }

            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }


    private void UpdatePortraitAndCharName()
    {
        if (index < portraits.Length && portraitImage != null)
        {
            portraitImage.sprite = portraits[index];
        }

        if (index < characterNames.Length && nameText != null)
        {
            nameText.text = characterNames[index];
        }
    }

    private void PlayVoice()
    {
        if (voiceSource != null && characterVoices.Length > 0)
        {
            int voiceIndex = Mathf.Min(index, characterVoices.Length - 1);
            AudioClip clip = characterVoices[voiceIndex];

            if (clip != null)
            {
                voiceSource.PlayOneShot(clip);
            }
        }
    }

    private void EndDialogue()
    {
        if (isBoss) {
            DialogueManager.hasSeenBlockDialogue = true; }
        if (isBoss)
        {
            DialogueManager.hasSeenBlockDialogueNotBoss = true;
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        isDialogue = false;
        //musicManager.PlayMusic();
    }


}


