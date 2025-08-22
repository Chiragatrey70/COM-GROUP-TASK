using UnityEngine;

// MemoryItem inherits from our reusable InteractableObject script.
public class MemoryItem : InteractableObject
{
    [Header("Memory Settings")]
    // This public field will let us link a specific pillow pile to this memory item.
    public GameObject pillowPileToVanish;

    // We override the Interact method to create our custom behavior.
    public override void Interact()
    {
        // Check if a pillow pile has been assigned in the Inspector.
        if (pillowPileToVanish != null)
        {
            // If it has, make the pillow pile disappear.
            Debug.Log("A memory was found. Clearing away " + pillowPileToVanish.name);
            pillowPileToVanish.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No pillow pile was assigned to this memory item!");
        }

        // After being used, the memory item disables itself so it can't be used again.
        gameObject.SetActive(false);
    }
}
