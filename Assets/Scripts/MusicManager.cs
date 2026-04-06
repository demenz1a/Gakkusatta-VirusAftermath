using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float fadeDuration = 2f;

    private Coroutine currentFade;

    void Start()
    {
        musicSource.enabled = true;
        //musicSource.volume = 0f;
        musicSource.Stop(); 
    }

    public void PlayMusic()
    {
        musicSource.Play();
        if (currentFade != null)
            StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeIn());
    }

    public void StopMusic()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(0f, 0.2f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 1f;
    }

    private IEnumerator FadeOut()
    {
        float startVolume = musicSource.volume;
        float time = 0f;
        while (time < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }
}
