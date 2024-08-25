using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Angle to open the door
    public float smooth = 2f; // Speed of door movement
    private bool isOpening = false; // State of door (opening/closing)
    private bool isClosed = true; // State of door (closed/opened)
    private Quaternion closedRotation; // Original rotation
    private Quaternion openRotation; // Target rotation

    void Start()
    {
        // Store the initial rotation of the door
        closedRotation = transform.rotation;
        // Calculate the open rotation based on the initial rotation and open angle
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // Handle door movement
        if (isOpening)
        {
            // Smoothly rotate towards the open rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * smooth);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false;
                isClosed = false;
            }
        }
        else if (!isOpening && !isClosed)
        {
            // Smoothly rotate towards the closed rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * smooth);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosed = true;
            }
        }

        // Check for player input to toggle the door state
        if (Input.GetKeyDown(KeyCode.E)) // Replace with your input method
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        // Toggle the door state
        isOpening = !isOpening;
        isClosed = !isClosed;
    }
}
