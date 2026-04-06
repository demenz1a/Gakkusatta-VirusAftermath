using UnityEngine;
using Unity.Cinemachine;

public class LinkSegments : MonoBehaviour
{
    public GameObject segmentsManagerPrefab;

    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {

            StartSegments();
        }
    }

    public void StartSegments()
    {
        GameObject managerObj = Instantiate(segmentsManagerPrefab);

        SegmentsManager segmentsManager = managerObj.GetComponent<SegmentsManager>();

        segmentsManager._player = player;
    }
}
