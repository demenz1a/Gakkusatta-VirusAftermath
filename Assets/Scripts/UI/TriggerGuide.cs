using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerGuide : MonoBehaviour
{
    //[Header("UI")]
    //[SerializeField] private GameObject pauseCanvas;
    //[SerializeField] private CanvasGroup fadeGroup;
    //[SerializeField] private Button resumeButton;
    //[SerializeField] private Button menuButton;

    [Header("Guide")]
    [SerializeField] private GameObject guideCanvas;
    [SerializeField] private Image background;
    [SerializeField] private ScrollRect guideScroll;
    [SerializeField] private Button resumeButton;
    [SerializeField] private TextMeshProUGUI guideText;

    public bool isGuide = false;

    void Awake()
    {
        guideCanvas.SetActive(false);
        //Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGuide)
            {
                Resume();
            }

             //return;
        }
    }

    public void Resume()
    {
        guideCanvas.SetActive(false);
        isGuide = false;
    }

    public void Guide()
    {
        guideCanvas.SetActive(true);
        //Time.timeScale = 0f;
        //LockCursor(false);
        isGuide = true;

        if (guideScroll) guideScroll.verticalNormalizedPosition = 1f;

        //fadeGroup.alpha = .6f;
    }
}
