using UnityEngine;

public class LockedDoor : InteractableObject
{
    [Header("Door Settings")]
    public int keysRequired = 3;
    public string lockedPrompt = "locked. Requires 3 keys.";
    public string unlockedPrompt = "open door";

    private bool isLocked = true;

    private void Start()
    {
        // Set the initial prompt when the game starts.
        interactionPrompt = lockedPrompt;
    }

    public override void Interact()
    {
        // Check with the GameManager to see if we have enough keys.
        if (GameManager.instance.keysCollected >= keysRequired)
        {
            isLocked = false;
        }

        if (isLocked)
        {
            // If it's locked, we don't do anything except show the prompt.
            // The PlayerController handles showing the text, so we just log a message here.
            Debug.Log("Door is locked. You need more keys.");
            interactionPrompt = lockedPrompt;
        }
        else
        {
            // If it's unlocked, open the door.
            Debug.Log("Door Unlocked! You Win!");

            // For now, "opening" the door just destroys it.
            // Later, we can add an animation here.
            Destroy(gameObject);
        }
    }

    // This is a special Unity function that is called every frame the player's
    // raycast is hitting this object's collider. We can use it to update the prompt.
    public void OnRaycastHit()
    {
        if (isLocked)
        {
            interactionPrompt = lockedPrompt;
        }
        else
        {
            interactionPrompt = unlockedPrompt;
        }
    }
}