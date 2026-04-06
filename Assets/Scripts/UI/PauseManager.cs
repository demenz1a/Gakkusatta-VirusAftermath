using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseCanvas;     
    [SerializeField] private CanvasGroup fadeGroup;       
    //[SerializeField] private ScrollRect guideScroll;    
    [SerializeField] private Button resumeButton;      
    [SerializeField] private Button menuButton;
    [SerializeField] private DialogueManager dialogueManager ;

    public TriggerGuide triggerGuide;

    private bool isPaused = false;

    void Awake()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;

        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (triggerGuide.isGuide)
           // {
               // triggerGuide.Resume();
             //   return;
           // }

            if (dialogueManager != null && dialogueManager.isDialogue)
            {
                ResumeInDialogue();
            }

            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        //LockCursor(true);
        isPaused = false;
    }

    public void ResumeInDialogue()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;            
        SceneManager.LoadScene("StartWorld");
    }


    private void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        //LockCursor(false);
        isPaused = true;

        //if (guideScroll) guideScroll.verticalNormalizedPosition = 1f;

        fadeGroup.alpha = .6f; 
    }

    //private static void LockCursor(bool locked)
    //{
    //    Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    //    Cursor.visible = !locked;
    //}
}
