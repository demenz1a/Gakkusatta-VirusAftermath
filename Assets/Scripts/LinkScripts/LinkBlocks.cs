using UnityEngine;
using Unity.Cinemachine;

public class LinkBlocks : MonoBehaviour
{
    public GameObject blocksManagerPrefab;

    public Transform spawnPoint;
    public CinemachineCamera virtualCamera;
    public Transform bossFocusPoint;
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            StartBlocks();
     }
   }

    public void StartBlocks()
    {

        GameObject managerObj = Instantiate(blocksManagerPrefab);

        BlocksManager blocksManager = managerObj.GetComponent<BlocksManager>();

        blocksManager.cinemachineCamera = virtualCamera;
        blocksManager.playerTransform = player;
        blocksManager.bossFocusPoint = bossFocusPoint;
        blocksManager.spawnPoint = spawnPoint;
    }
}

