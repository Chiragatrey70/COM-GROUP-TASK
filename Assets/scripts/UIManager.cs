using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panel References")]
    public GameObject fileViewPanel; // The parent panel for the file view
    public TextMeshProUGUI fileContentText; // The text element to display content

    [Header("Player Reference")]
    public PlayerController playerController; // Reference to the player controller script

    private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // This method is called by a patient file to display its content
    public void ShowFile(string content)
    {
        fileViewPanel.SetActive(true);
        fileContentText.text = content;

        // Pause player movement and unlock cursor
        playerController.SetMovement(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // This method will be called by our "Close" button
    public void HideFile()
    {
        fileViewPanel.SetActive(false);

        // Resume player movement and lock cursor
        playerController.SetMovement(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
