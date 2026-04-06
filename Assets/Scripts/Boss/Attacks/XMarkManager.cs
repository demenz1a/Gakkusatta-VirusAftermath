using UnityEngine;

public class XMarkManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;    
    [SerializeField] private GameObject _xmarkPrefab;
    [SerializeField] private AudioClip _spawnClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = 1.0f;
    }

    public void StartXMark()
    {
        _audioSource.PlayOneShot(_spawnClip);
        SpawnXmark();
    }

    private void SpawnXmark()
    {
        GameObject xmarkClone = Instantiate(_xmarkPrefab, _player.transform.position, Quaternion.identity, _player.transform);
    }
}
