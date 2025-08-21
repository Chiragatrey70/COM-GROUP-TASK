using UnityEngine;

// KeyItem inherits from InteractableObject, so it gets all its functionality.
public class KeyItem : InteractableObject
{
    // We override the Interact method to add our own custom logic.
    public override void Interact()
    {
        // Find the GameManager instance and tell it we've collected a key.
        GameManager.instance.AddKey();

        // We still want the object to disappear, so we call the original
        // Interact method from the base class (InteractableObject).
        base.Interact();
    }
}
