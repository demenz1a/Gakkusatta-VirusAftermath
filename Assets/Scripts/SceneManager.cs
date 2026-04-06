using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public static Scene1Manager Instance;

    public int ReikoHP = 0;
    public int MinoriHP = 100;
    public bool firstReturnFromLevel2 = true;
    public Vector3 playerPosition;
    public int killCounter = 0;
    public bool isFirstStart = true;
    public bool firstSwitch = true;

    public string returnSpawnID = "Default"; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}

