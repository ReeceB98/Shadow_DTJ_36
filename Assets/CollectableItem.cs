using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CollectibleItem : MonoBehaviour
{
    public TextMeshProUGUI pickupMessage; // For TextMeshProUGUI
    public string keyID; // Unique identifier for the key

    public bool isCollected = false;
    public float pickupDistance = 2.5f;

    private bool canPickUp = false;
    private Transform playerTransform;
    private static List<string> collectedKeys = new List<string>();

    private void Start()
    {
        if (pickupMessage != null)
        {
            pickupMessage.enabled = false;
        }
        playerTransform = Camera.main.transform; // Assuming the player camera is the main camera
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!isCollected && distanceToPlayer <= pickupDistance)
        {
            if (pickupMessage != null)
            {
                pickupMessage.text = "Press E to pick up";
                pickupMessage.enabled = true;
            }

            canPickUp = true;
        }
        else
        {
            if (pickupMessage != null)
            {
                pickupMessage.enabled = false;
            }

            canPickUp = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && canPickUp)
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        isCollected = true;
        gameObject.SetActive(false);

        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
        }

        // Ensure pickup message is hidden when the item is collected
        if (pickupMessage != null)
        {
            pickupMessage.enabled = false;
        }
    }

    public static bool HasKey(string keyID)
    {
        return collectedKeys.Contains(keyID);
    }
}
