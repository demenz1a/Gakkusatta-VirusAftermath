using Unity.Cinemachine;
using UnityEngine;

public class LinkXmark : MonoBehaviour
{
    public GameObject xMarkPrefab;

    public Transform spawnPoint;
    public CinemachineCamera virtualCamera;
    public Transform bossFocusPoint;
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            StartXmark();
        }
    }

    public void StartXmark()
    {
    }
}
