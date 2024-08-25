using UnityEngine;
using TMPro;

public class LeverController : MonoBehaviour
{
    public float leverAngle = 90f; // Angle to move the lever downwards
    public float smooth = 2f; // Speed of lever movement
    public float interactionDistance = 2.5f; // Distance within which the player can interact with the lever
    public TextMeshProUGUI leverMessage; // Message to display for interacting with the lever
    public Fusebox fusebox; // Reference to the Fusebox script to check if the fuse is placed
    public GameObject greenLight; // Green light GameObject that activates when the lever is pulled
    public LargeGateController gate; // Reference to the LargeGateController script for the gate

    private bool isLeverDown = false; // State of the lever
    private Quaternion upPosition;
    private Quaternion downPosition;
    private Transform playerTransform;

    private void Start()
    {
        upPosition = transform.rotation; // The starting position of the lever
        downPosition = Quaternion.Euler(transform.eulerAngles + new Vector3(leverAngle, 0, 0)); // Downward position

        playerTransform = Camera.main.transform; // Assuming the player camera is the main camera

        if (leverMessage != null)
        {
            leverMessage.enabled = false; // Hide the message initially
        }

        if (greenLight != null)
        {
            greenLight.SetActive(false); // Ensure the green light is off initially
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (fusebox != null && fusebox.IsFusePlaced())
            {
                ShowPullLeverMessage(); // Show "Press P to pull the lever" message

                if (Input.GetKeyDown(KeyCode.P))
                {
                    ToggleLever(); // Pull the lever
                }
            }
            else
            {
                ShowNoFuseMessage(); // Show message if the fuse is not placed
            }
        }
        else
        {
            // Hide the message if the player is not in range
            if (leverMessage != null)
            {
                leverMessage.enabled = false;
            }
        }

        // Handle lever movement
        if (isLeverDown)
        {
            // Smoothly rotate the lever to the downward position
            transform.rotation = Quaternion.Slerp(transform.rotation, downPosition, Time.deltaTime * smooth);
        }
        else
        {
            // Smoothly rotate the lever to the upward position
            transform.rotation = Quaternion.Slerp(transform.rotation, upPosition, Time.deltaTime * smooth);
        }
    }

    private void ShowPullLeverMessage()
    {
        if (leverMessage != null && !isLeverDown)
        {
            leverMessage.text = "Press P to pull the lever";
            leverMessage.enabled = true;
        }
    }

    private void ShowNoFuseMessage()
    {
        if (leverMessage != null)
        {
            leverMessage.text = "";
            leverMessage.enabled = true;
        }
    }

    private void ToggleLever()
    {
        if (!isLeverDown)
        {
            isLeverDown = true; // Move the lever down
            Debug.Log("Lever pulled down.");

            // Activate the green light when the lever is pulled
            if (greenLight != null)
            {
                greenLight.SetActive(true); // Turn on the green light
            }

            // Open the large gate when the lever is pulled
            if (gate != null)
            {
                gate.OpenGate(); // Call the method to open the gate
            }

            // Hide the lever message after the lever is pulled
            if (leverMessage != null)
            {
                leverMessage.enabled = false;
            }

            // Optionally, disable further interaction if desired
            // enabled = false; // Uncomment if you want to disable the script after pulling the lever
        }
    }
}
