using UnityEngine;

public class StartGachaSplash : MonoBehaviour
{
        [SerializeField] private GameObject GachaAnim;
        [SerializeField] private GameObject GachaSplashBack;

    public void StartGacha()
    {
        Instantiate(GachaAnim);
        Instantiate(GachaSplashBack);
    }
}
