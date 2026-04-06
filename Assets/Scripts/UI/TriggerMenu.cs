using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMenu : MonoBehaviour
{
    private float timer = -1f; 

    public AudioSource music;
    public GameObject black;

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.5f)
            {
                //Instantiate(black);
                music.Stop();
            }

            if (timer <= 0)
            {
                SceneManager.LoadScene("StartWorld");
            }
        }
    }

    public void StartMenu()
    {
       SceneManager.LoadScene("StartWorld");
    }
}

