using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class FiguresCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private Transform playerTransform;

    private bool hasFocused = false;

    public void FocusOnce(Transform target, float duration)
    {
        if (hasFocused) return;

        StartCoroutine(FocusRoutine(target, duration));
        hasFocused = true;
    }

    private IEnumerator FocusRoutine(Transform target, float duration)
    {
        virtualCamera.LookAt = target;

        yield return new WaitForSeconds(duration);

        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
    }
    public void ResetFocus()
    {
        hasFocused = false;
    }
}




