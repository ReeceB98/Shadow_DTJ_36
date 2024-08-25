using UnityEngine;
using TMPro;

public class Fusebox : MonoBehaviour
{
    public TextMeshProUGUI fuseboxMessage; // For TextMeshProUGUI
    public string requiredFuseID; // The ID of the fuse that should be placed in this fusebox
    public float interactionDistance = 2.5f;

    public GameObject fuseObjectInBox; // Physical fuse that is initially turned off and activates when placed
    private bool fusePlaced = false; // State of fuse placement
    private Transform playerTransform;

    private void Start()
    {
        if (fuseboxMessage != null)
        {
            fuseboxMessage.enabled = false;
        }

        playerTransform = Camera.main.transform; // Assuming the player camera is the main camera

        // Ensure the fuse object in the box is disabled at the start
        if (fuseObjectInBox != null)
        {
            fuseObjectInBox.SetActive(false); // Initially turned off
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!fusePlaced && distanceToPlayer <= interactionDistance)
        {
            // If the player has the required fuse
            if (CollectibleItem.HasFuse(requiredFuseID))
            {
                ShowPlaceFuseMessage(); // Show "Press P to place fuse" message

                if (Input.GetKeyDown(KeyCode.P))
                {
                    PlaceFuse(); // Player places the fuse
                }
            }
            else
            {
                ShowFuseNeededMessage(); // Show "I need to find a fuse..." message if the player doesn't have the fuse
            }
        }
        else if (fusePlaced && distanceToPlayer <= interactionDistance)
        {
            // Once the fuse is placed, stop showing any interaction message
            if (fuseboxMessage != null)
            {
                fuseboxMessage.enabled = false;
            }
        }
        else
        {
            if (fuseboxMessage != null)
            {
                fuseboxMessage.enabled = false; // Disable message if the player is not in range
            }
        }
    }

    private void ShowFuseNeededMessage()
    {
        if (fuseboxMessage != null && !fusePlaced)
        {
            fuseboxMessage.text = "I need to find a fuse...";
            fuseboxMessage.enabled = true;
        }
    }

    private void ShowPlaceFuseMessage()
    {
        if (fuseboxMessage != null && !fusePlaced)
        {
            fuseboxMessage.text = "Press P to place fuse";
            fuseboxMessage.enabled = true;
        }
    }

    private void PlaceFuse()
    {
        fusePlaced = true;
        Debug.Log("Fuse placed in fusebox.");

        // Disable the fusebox message after the fuse is placed
        if (fuseboxMessage != null)
        {
            fuseboxMessage.enabled = false;
        }

        // Activate the physical fuse object in the fusebox
        if (fuseObjectInBox != null)
        {
            fuseObjectInBox.SetActive(true); // Activate (turn on) the fuse object
        }

        // Disable the CollectibleItem script on the fuse object
        GameObject fuseObject = GameObject.Find("fuse-box-fuse (1)"); // Replace with your fuse object name
        if (fuseObject != null)
        {
            CollectibleItem collectibleItem = fuseObject.GetComponent<CollectibleItem>();
            if (collectibleItem != null)
            {
                collectibleItem.enabled = false; // Disable the CollectibleItem script
            }
        }

        // Ensure the fuse can't be removed by deactivating further interaction
        enabled = false; // Disable the script to stop further interactions
    }

    public bool IsFusePlaced()
    {
        return fusePlaced;
    }
}
