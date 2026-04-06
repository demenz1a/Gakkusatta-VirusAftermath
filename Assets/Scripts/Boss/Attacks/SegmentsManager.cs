using System.Collections;
using UnityEngine;

public class SegmentsManager : MonoBehaviour
{   
    public Transform _player;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private int _laserCount = 5;
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private AudioClip _activateClip;
    private AudioSource _audioSource;

    public bool IsAttackFinished { get; private set; }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = 1.0f;
    }

    public void StartSegments()
    {
        IsAttackFinished = false;
        StartCoroutine(SpawnSegments());
    }

    IEnumerator SpawnSegments()
    {
        for (int i = 0; i < _laserCount; i++)
        {
            _audioSource.PlayOneShot(_activateClip);
            SpawnSegmentsNearPlayer();
            yield return new WaitForSeconds(_spawnInterval);
        }
        yield return new WaitForSeconds(2f);

        IsAttackFinished = true;
    }

    private void SpawnSegmentsNearPlayer()
    {
        if (_player == null) return;

        Vector2 offset2D = Random.insideUnitCircle * _spawnRadius;
        Vector3 offset = new Vector3(offset2D.x, offset2D.y, 0);
        Vector3 spawnPosition = _player.position + offset;

        float randomAngle = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);

        Instantiate(_laserPrefab, spawnPosition, randomRotation);
    }

}

