using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package installed

public class CarInteraction : MonoBehaviour
{
    public TextMeshProUGUI interactionMessage; // UI TextMeshPro component for displaying messages
    public float interactionDistance = 3.0f; // Distance within which the player can interact with the car
    private Transform playerTransform; // Reference to the player transform

    private void Start()
    {
        // Ensure the interaction message is hidden at the start
        if (interactionMessage != null)
        {
            interactionMessage.enabled = false;
        }

        // Assume the player camera is the main camera
        playerTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Calculate the distance between the player and the car
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            ShowInteractionMessage(); // Show the message if the player is within interaction distance

            if (Input.GetKeyDown(KeyCode.E))
            {
                EscapeCar(); // Call method when player presses E
            }
        }
        else
        {
            HideInteractionMessage(); // Hide the message if the player is not in range
        }
    }

    private void ShowInteractionMessage()
    {
        if (interactionMessage != null)
        {
            interactionMessage.text = "Press E to escape!";
            interactionMessage.enabled = true;
        }
    }

    private void HideInteractionMessage()
    {
        if (interactionMessage != null)
        {
            interactionMessage.enabled = false;
        }
    }

    private void EscapeCar()
    {
        // Implement what should happen when the player escapes the car
        Debug.Log("Player pressed E to escape the car!");
        // You can add code here to handle the escape logic (e.g., transition to a different scene, enable player controls, etc.)
    }
}
