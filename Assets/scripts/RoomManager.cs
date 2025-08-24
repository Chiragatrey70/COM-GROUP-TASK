using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room Objects")]
    public GameObject patientToSpawn;
    public GameObject[] pillowPilesToVanish;

    [Header("Puzzle State")]
    public int memoryItemsRequired = 3; // Set this to the number of items in the room
    private int memoryItemsFound = 0;

    void Start()
    {
        // Make sure the patient is hidden when the room starts
        if (patientToSpawn != null)
        {
            patientToSpawn.SetActive(false);
        }
    }

    // This method will be called by each memory item when it's found
    public void OnMemoryItemFound()
    {
        memoryItemsFound++;

        // Check if all items have been found
        if (memoryItemsFound >= memoryItemsRequired)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("All memories found! The patient's spirit appears.");

        // Vanish all the pillow piles
        foreach (GameObject pile in pillowPilesToVanish)
        {
            if (pile != null)
            {
                pile.SetActive(false);
            }
        }

        // Spawn the patient
        if (patientToSpawn != null)
        {
            patientToSpawn.SetActive(true);
        }
    }
}
