 using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Katana katana;

    private void Awake()
    {
        Instance = this;
    }

    public Katana GetActiveWeapon()
    {
        return katana;
    }
}