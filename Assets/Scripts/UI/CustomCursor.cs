using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Header("Follow Settings")]
    public float acceleration = 50f;
    public float drag = 10f;
    [SerializeField] private Camera mainCamera;

    private Vector3 velocity;
    private bool isActive = false;
    public Animator animator;

    private void Awake()
    {
        Cursor.visible = true; 
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isActive || mainCamera == null) return;

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -mainCamera.transform.position.z
        ));
        mousePos.z = 0f;

        Vector3 dir = mousePos - transform.position;
        velocity += dir * acceleration * Time.deltaTime;
        velocity *= 1f - drag * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    public void ShowCursor()
    {
        Cursor.visible = false;
        gameObject.SetActive(true);
        isActive = true;
        if (animator) animator.SetTrigger("Appear");
    }

    public void HideCursor()
    {
        animator.SetTrigger("Appear");
    }

    public void HideCursorFunction()
    {
        gameObject.SetActive(false);
        Cursor.visible = true;
    }

    public void ReturnToDefault()
    {
        isActive = false;
        gameObject.SetActive(false);
        Cursor.visible = true;
    }
}



