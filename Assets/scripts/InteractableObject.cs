using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // This is a public message that can be changed in the Inspector for each item.
    public string interactionPrompt = "pick up";

    // This is the main function that our player will call.
    // The 'virtual' keyword allows other scripts to override this function
    // if we want different items to have more complex, unique behaviors.
    public virtual void Interact()
    {
        // For now, we'll just log a message to the console to confirm it works.
        Debug.Log("Interacted with: " + gameObject.name);

        // Destroy the object to simulate picking it up.
        Destroy(gameObject);
    }
}
