using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    [SerializeField] private string SceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneName);
    }
}
