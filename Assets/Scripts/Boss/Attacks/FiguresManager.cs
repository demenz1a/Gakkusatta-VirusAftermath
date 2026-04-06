using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FigureManager : MonoBehaviour
{
    public bool IsAttackFinished { get; private set; } = true;
    [SerializeField] private List<GameObject> _figurePrefabs;
    [SerializeField] private Transform _spawnOrigin;
    [SerializeField] private float _spacing = 2f;
    [SerializeField] private FiguresCameraController _cameraController;   
    [SerializeField] private AudioClip _spawnClip;
    private List<Figures> _spawnedFigures = new List<Figures>();
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = 1.0f;
    }
    private void Update()
    {

        CheckIfAllFiguresFinished();
    }

    public void StartFigures()
    {
        IsAttackFinished = false;
        _cameraController?.ResetFocus();
        CleanupOldFigures();
        SpawnAllFigures();
        StartCoroutine(ActivateFiguresSequentially());
        _audioSource.PlayOneShot(_spawnClip);
    }

    private void CleanupOldFigures()
    {
        foreach (var fig in _spawnedFigures)
        {
            if (fig != null)
                Destroy(fig.gameObject);
        }

        _spawnedFigures.Clear();
    }

    private void SpawnAllFigures()
    {
        for (int i = 0; i < _figurePrefabs.Count; i++)
        {
            Vector3 spawnPos = _spawnOrigin.position + Vector3.right * _spacing * i;
            GameObject figure = Instantiate(_figurePrefabs[i], spawnPos, Quaternion.identity);
            Figures figureScript = figure.GetComponent<Figures>();
            _spawnedFigures.Add(figureScript);
        }
    }

    private IEnumerator ActivateFiguresSequentially()
    {
        for (int i = 0; i < _spawnedFigures.Count; i++)
        {
            Figures figure = _spawnedFigures[i];

            if (figure == null) continue;

            if (_cameraController != null)
            {
                _cameraController.FocusOnce(figure.transform, 1.5f);
                yield return new WaitForSeconds(1.5f);
            }

            figure.Activate();
            yield return new WaitForSeconds(2f);
        }
    }

    private void CheckIfAllFiguresFinished()
    {
        if (IsAttackFinished) return;

        bool allFinished = true;

        foreach (var figure in _spawnedFigures)
        {
            if (figure != null && figure.gameObject.activeSelf && !figure.IsAttackFinished)
            {
                allFinished = false;
                break;
            }
        }

        if (allFinished)
        {
            IsAttackFinished = true;
            Debug.Log("All figure attacks finished!");
        }
    }
}




