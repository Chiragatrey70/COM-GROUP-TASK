using UnityEngine;

public class ViewableMemoryItem : InteractableObject
{
    [Header("Item Details")]
    public Sprite itemSprite;
    [TextArea(3, 5)]
    public string itemDescription;

    [Header("Room Logic")]
    // We need a reference to the RoomManager
    public RoomManager roomManager;

    // When the player first clicks, we just show the UI.
    public override void Interact()
    {
        UIManager.instance.ShowMemoryItem(this);
    }

    // This method is called by the UIManager AFTER the player closes the view.
    public void CompleteInteraction()
    {
        // Tell the RoomManager that this item has been found.
        if (roomManager != null)
        {
            roomManager.OnMemoryItemFound();
        }
        else
        {
            Debug.LogError("RoomManager not assigned on this memory item!");
        }

        // Disable the object so it can't be used again.
        gameObject.SetActive(false);
    }
}
