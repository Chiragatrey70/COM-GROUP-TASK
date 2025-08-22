using UnityEngine;
using UnityEngine.UI; // Needed for the Image component
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("File Viewer Panel")]
    public GameObject fileViewPanel;
    public TextMeshProUGUI fileContentText;

    [Header("Item Viewer Panel")] // NEW SECTION
    public GameObject itemViewPanel;
    public Image itemImage;
    public TextMeshProUGUI itemDescriptionText;

    [Header("Player Reference")]
    public PlayerController playerController;

    // We need a temporary place to store the memory item we are viewing
    private ViewableMemoryItem currentMemoryItem;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // --- File Viewer Methods ---
    public void ShowFile(string content)
    {
        fileViewPanel.SetActive(true);
        fileContentText.text = content;
        PauseGame();
    }

    public void HideFile()
    {
        fileViewPanel.SetActive(false);
        ResumeGame();
    }

    // --- Item Viewer Methods --- NEW
    public void ShowMemoryItem(ViewableMemoryItem memoryItem)
    {
        itemViewPanel.SetActive(true);
        // We'll need a placeholder sprite for the image for now
        itemImage.sprite = memoryItem.itemSprite;
        itemDescriptionText.text = memoryItem.itemDescription;

        // Store the item so we know which one to activate when the UI closes
        currentMemoryItem = memoryItem;
        PauseGame();
    }

    public void HideMemoryItem()
    {
        itemViewPanel.SetActive(false);

        // When the player closes the view, trigger the item's effect
        if (currentMemoryItem != null)
        {
            currentMemoryItem.CompleteInteraction();
            currentMemoryItem = null; // Clear the reference
        }

        ResumeGame();
    }


    // --- Helper Methods ---
    private void PauseGame()
    {
        playerController.SetMovement(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        playerController.SetMovement(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
