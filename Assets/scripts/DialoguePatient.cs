using UnityEngine;

public class DialoguePatient : InteractableObject
{
    [Header("Dialogue")]
    [TextArea(5, 10)]
    public string dialogueText;

    [Header("Reward")]
    public GameObject keyToSpawn;

    void Start()
    {
        // Make sure the key is hidden at the start
        if (keyToSpawn != null)
        {
            keyToSpawn.SetActive(false);
        }
    }

    public override void Interact()
    {
        // We'll reuse the File Viewer UI to show the dialogue.
        UIManager.instance.ShowFile(dialogueText);

        // After showing the dialogue, spawn the key and disappear.
        if (keyToSpawn != null)
        {
            keyToSpawn.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
