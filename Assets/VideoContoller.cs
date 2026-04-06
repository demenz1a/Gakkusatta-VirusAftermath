using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // Ссылка на компонент VideoPlayer
   public GameObject prefabToSpawn;
    public GameObject prefabToSpawn2; // Префаб, который нужно вызвать после видео
                                     //public Transform spawnPoint;          // Точка, где появится префаб

    private void Start()
    {
        // Подписываемся на событие окончания видео
        videoPlayer.loopPointReached += OnVideoFinished;

        // Запускаем воспроизведение
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Спавним объект после окончания видео
        Instantiate(prefabToSpawn);
        Instantiate(prefabToSpawn2);

        // Можно убрать видеообъект
        Destroy(videoPlayer.gameObject);
    }
}

