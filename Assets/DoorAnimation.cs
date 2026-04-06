using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;      // На сколько градусов открывается
    [SerializeField] private float openSpeed = 2f;       // Скорость открытия
    [SerializeField] private bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isAnimating = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenDoor();
        }
    }

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void OpenDoor()
    {
        if (!isAnimating && !isOpen)
            StartCoroutine(AnimateDoor(openRotation, true));
    }

    public void CloseDoor()
    {
        if (!isAnimating && isOpen)
            StartCoroutine(AnimateDoor(closedRotation, false));
    }

    private System.Collections.IEnumerator AnimateDoor(Quaternion targetRotation, bool opening)
    {
        isAnimating = true;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = opening;
        isAnimating = false;
    }
}

