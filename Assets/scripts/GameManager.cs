using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the static instance of the GameManager that can be accessed from anywhere.
    public static GameManager instance;

    // The number of keys the player has collected.
    public int keysCollected = 0;

    // Awake is called before the first frame update, even before Start().
    private void Awake()
    {
        // This is the singleton pattern.
        // If an instance of this script already exists and it's not this one, destroy this one.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set the instance to this one.
            instance = this;
        }
    }

    // A public method that other scripts can call to add a key.
    public void AddKey()
    {
        keysCollected++;
        Debug.Log("Key collected! Total keys: " + keysCollected);
    }
}
