using UnityEngine;

public class SpawnSplashForGacha : MonoBehaviour
{
    [SerializeField] private GameObject SplashCanvas; 

    public void SpawmSplash()
    {
        Instantiate(SplashCanvas);
    }
}
