using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [SerializeField] private GameObject shot;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CustomCursor customCursor;
    public bool isGun;
    private Vector3 targetPos;
    

    private void Awake()
    {
        Instance = this;
    }

    public void GunShot()
    {
        if (isGun)
        {
            Vector3 spawnPosition = customCursor.transform.position;
            spawnPosition.z = 0f; 

            Instantiate(shot, spawnPosition, Quaternion.identity);
        }
    }
}

