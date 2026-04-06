using Unity.Cinemachine;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;

    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float force = 1f)
    {
        impulseSource.GenerateImpulse(force);
    }
}

