using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Angle to open the door
    public float smooth = 2f; // Speed of door movement
    public string requiredKeyID; // Key ID needed to unlock this door (if locked)
    public bool isLocked = true; // Whether the door is locked or not
    public float interactionDistance = 3f; // Distance within which the player can interact with the door

    private bool isOpening = false;
    private bool isClosed = true;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Transform playerTransform;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        playerTransform = Camera.main.transform; // Assuming the player camera is the main camera
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (isOpening)
            {
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
                transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * smooth);
                if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
                {
                    transform.rotation = closedRotation;
                    isClosed = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) // Replace with your input method
            {
                ToggleDoor();
            }
        }
    }

    void ToggleDoor()
    {
        if (isLocked)
        {
            // Check if the player has the correct key if the door is locked
            if (CollectibleItem.HasKey(requiredKeyID))
            {
                isOpening = !isOpening;
                isClosed = !isClosed;
            }
            else
            {
                Debug.Log("You need the correct key to open this door.");
            }
        }
        else
        {
            // If the door is not locked, just toggle the door state
            isOpening = !isOpening;
            isClosed = !isClosed;
        }
    }
}
