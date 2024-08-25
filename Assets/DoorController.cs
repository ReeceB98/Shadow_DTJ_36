using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Angle to open the door
    public float smooth = 2f; // Speed of door movement
    public string requiredKeyID; // Key ID needed to unlock this door (if locked)
    public bool isLocked = true; // Whether the door is locked or not
    public float interactionDistance = 3f; // Distance within which the player can interact with the door

    public AudioClip openSound; // Sound to play when the door opens
    public AudioClip closeSound; // Sound to play when the door closes
    public AudioClip lockedSound; // Sound to play when the door is locked
    public AudioClip unlockSound; // Sound to play when the door is unlocked with a key

    public float openSoundVolume = 1f; // Volume for the open sound
    public float closeSoundVolume = 1f; // Volume for the close sound
    public float lockedSoundVolume = 1f; // Volume for the locked sound
    public float unlockSoundVolume = 1f; // Volume for the unlock sound

    private AudioSource audioSource; // AudioSource component
    private bool isOpening = false;
    private bool isClosing = false;
    private bool unlockSoundPlayed = false; // Flag to track if unlock sound has been played
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Transform playerTransform;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        playerTransform = Camera.main.transform; // Assuming the player camera is the main camera

        // Add an AudioSource component if it doesn't already exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance && Input.GetKeyDown(KeyCode.E)) // Replace with your input method
        {
            ToggleDoor();
        }

        // Handle door movement
        if (isOpening)
        {
            // Smoothly rotate the door to the open position
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * smooth);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false;
            }
        }
        else if (isClosing)
        {
            // Smoothly rotate the door to the closed position
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * smooth);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosing = false;
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
                if (!unlockSoundPlayed)
                {
                    PlaySound(unlockSound, unlockSoundVolume); // Play unlock sound only once
                    unlockSoundPlayed = true;
                }

                // Toggle door state based on current state
                if (isOpening || isClosing)
                {
                    // If the door is currently moving, stop it and reset to closed
                    StopDoorMovement();
                    isClosing = true;
                }
                else
                {
                    isOpening = !isOpening;
                }
            }
            else
            {
                PlaySound(lockedSound, lockedSoundVolume); // Play locked sound
                Debug.Log("You need the correct key to open this door.");
            }
        }
        else
        {
            // If the door is not locked, toggle the door state based on current state
            if (isOpening || isClosing)
            {
                StopDoorMovement();
                isClosing = true;
            }
            else
            {
                isOpening = !isOpening;
            }
        }

        // Play the open or close sound based on the state change
        if (isOpening)
        {
            PlaySound(openSound, openSoundVolume); // Play open sound
        }
        else if (isClosing)
        {
            PlaySound(closeSound, closeSoundVolume); // Play close sound
        }
    }

    void StopDoorMovement()
    {
        isOpening = false;
        isClosing = false;
        // Ensure no ongoing sound is playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is missing.");
        }
    }
}
