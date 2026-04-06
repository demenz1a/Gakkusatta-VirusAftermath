using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerStart : MonoBehaviour
{
    private float timer = -1f;

    public AudioSource music;
    public GameObject black;

    private bool blackSpawned = false; 

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.5f && !blackSpawned)
            {
                Instantiate(black);
                music.Stop();
                blackSpawned = true; 
            }

            if (timer <= 0)
            {
                SceneManager.LoadScene("FightSchoolWorld");
            }
        }
    }

    public void StartGame()
    {
        timer = 0.5f;
        blackSpawned = false;
    }

    public void StartGameWithBlack()
    {
        Instantiate(black);
    }
}


