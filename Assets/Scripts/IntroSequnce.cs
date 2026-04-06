using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] string WorldName;
    [SerializeField] float Time;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadStartWorld();
        }
    }
    void Start()
    {
        Invoke("LoadStartWorld", Time);
    }

    void LoadStartWorld()
    {
        SceneManager.LoadScene(WorldName);
    }

}