using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] bars;
    public GameObject click;
    public Gun gun;
    private int currentIndex = 0;
    public static CharacterSwitch instance;
    private bool oneIsDead = false;
    [SerializeField] private DialogueManager dialogueManager;

    public static CharacterSwitch Instance => instance;

    public GameObject CurrentCharacter => characters[currentIndex];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == currentIndex);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && SceneStorage.shouldEnableObject == true && !oneIsDead)
        {
            Switch();
        }
    }

    public void Switch()
    {
        Vector3 oldPos = characters[currentIndex].transform.position;
        Quaternion oldRot = characters[currentIndex].transform.rotation;

        characters[currentIndex].SetActive(false);
        bars[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % characters.Length;

        characters[currentIndex].transform.SetPositionAndRotation(oldPos, oldRot);

        characters[currentIndex].SetActive(true);
        bars[currentIndex].SetActive(true);

        if (click != null)
        {
            click.SetActive(false);
        }

        gun.isGun = !gun.isGun;

        if (Scene1Manager.Instance.firstSwitch)
        {
            Scene1Manager.Instance.firstSwitch = false; 
            if (dialogueManager != null)
            {
                StartCoroutine(ShowMinoriDialogue());
            }
        }
    }


    private void ForceSwitchTo(int index)
    {
        characters[currentIndex].SetActive(false);
        bars[currentIndex].SetActive(false);

        currentIndex = index;

        characters[currentIndex].SetActive(true);
        bars[currentIndex].SetActive(true);

        if (click != null) click.SetActive(false);
        gun.isGun = !gun.isGun;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LoadWinnerWorldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DeadWorld");
    }

    private IEnumerator ShowMinoriDialogue()
    {

        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogueFromIndex(4);

        yield return new WaitUntil(() => !dialogueManager.gameObject.activeSelf);
    }
}





