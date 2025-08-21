using UnityEngine;

public class PatientFile : InteractableObject
{
    [Header("File Content")]
    // The [TextArea] attribute gives us a bigger box in the Inspector to write in.
    [TextArea(5, 10)]
    public string fileContent;

    public override void Interact()
    {
        // Tell the UIManager to show our content.
        UIManager.instance.ShowFile(fileContent);

        // By removing the "gameObject.SetActive(false);" line from here,
        // the file will remain in the world after you close the UI,
        // allowing you to interact with it again.
    }
}
