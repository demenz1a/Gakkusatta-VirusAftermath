using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class LinkScheduleDance : MonoBehaviour
{
    public GameObject _scheduleDanceManagerPrefab;
    public CinemachineCamera bossCam;
    public CinemachineCamera playerCam;
    public Animator _animator;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {

    //        StartDance();
    //    }
    //}

    public void StartDance()
    {
        GameObject managerObj = Instantiate(_scheduleDanceManagerPrefab);

        ScheduleDanceManager danceManager = managerObj.GetComponent<ScheduleDanceManager>();
    }
}

