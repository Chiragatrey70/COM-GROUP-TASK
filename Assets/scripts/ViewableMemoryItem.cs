using UnityEngine;

public class ViewableMemoryItem : InteractableObject
{
    [Header("Item Details")]
    public Sprite itemSprite; // The image to display in the UI
    [TextArea(3, 5)]
    public string itemDescription; // The text to display

    [Header("Minigame Logic")]
    public GameObject pillowPileToVanish;

    // When the player first clicks, we just show the UI.
    public override void Interact()
    {
        UIManager.instance.ShowMemoryItem(this);
    }

    // This method is called by the UIManager AFTER the player closes the view.
    public void CompleteInteraction()
    {
        if (pillowPileToVanish != null)
        {
            Debug.Log("A memory was viewed. Clearing away " + pillowPileToVanish.name);
            pillowPileToVanish.SetActive(false);
        }

        // Disable the object so it can't be used again.
        gameObject.SetActive(false);
    }
}
