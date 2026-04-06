using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOST : MonoBehaviour
{
    private static GameOST instance;
    private AudioSource audioSource;

    [SerializeField] private string[] scenesWithoutMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string s in scenesWithoutMusic)
        {
            if (scene.name == s)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
                return;
            }
        }

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

