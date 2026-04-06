using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  
    [SerializeField] private float scaleAmount = 0.2f; 

    private Vector3 _startScale;

    void Start()
    {
        _startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = _startScale * scale;
    }
}

