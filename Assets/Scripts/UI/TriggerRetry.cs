using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerRetry : MonoBehaviour
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
                Instantiate(black);
                music.Stop();
            }

            if (timer <= 0)
            {
                SceneManager.LoadScene("FightSchoolWorld");
            }
        }
    }

    public void StartGame()
    {
        Scene1Manager.Instance.ReikoHP = 300;
        Scene1Manager.Instance.MinoriHP = 300;
        Scene1Manager.Instance.killCounter = 1;
        timer = 0.5f;
    }
}
