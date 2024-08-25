using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    // UI Text for displaying the message
    public TextMeshProUGUI pickupMessage;

    // To check if the object has been collected
    public bool isCollected = false;

    // Reference to the outline script
    private Outline outlineScript;

    // Distance threshold for interacting with the object
    public float pickupDistance = 2.5f;

    // Whether the player is close enough to collect the item
    private bool canPickUp = false;

    private void Start()
    {
        // Hide the pickup message initially
        if (pickupMessage != null)
        {
            pickupMessage.enabled = false;
        }

        // Get the Outline component from this object
        outlineScript = GetComponent<Outline>();
    }

    private void Update()
    {
        // Check if the object is being highlighted by the outline script
        if (outlineScript != null && outlineScript.isOutlined && !isCollected)
        {
            // Get the distance between the player and the object
            float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

            // Check if the player is close enough to pick up the item
            if (distanceToPlayer <= pickupDistance)
            {
                // Enable the "Press E to pick up" message
                if (pickupMessage != null)
                {
                    pickupMessage.text = "Press 'E' to pick up";
                    pickupMessage.enabled = true;
                }

                canPickUp = true;
            }
            else
            {
                // Hide the message if the player is too far away
                if (pickupMessage != null)
                {
                    pickupMessage.enabled = false;
                }

                canPickUp = false;
            }

            // Check if the player presses the 'E' key and is within range
            if (Input.GetKeyDown(KeyCode.E) && canPickUp)
            {
                CollectItem();
            }
        }
        else
        {
            // Hide the pickup message if the item is not highlighted
            if (pickupMessage != null)
            {
                pickupMessage.enabled = false;
            }
        }
    }

    private void CollectItem()
    {
        // Set the item as collected
        isCollected = true;

        // Optionally: Play a sound, animation, or trigger an event

        // Hide the object
        gameObject.SetActive(false);

        // Hide the pickup message
        if (pickupMessage != null)
        {
            pickupMessage.enabled = false;
        }

        // Additional logic for when the item is collected
        Debug.Log(gameObject.name + " has been collected.");
    }
}
