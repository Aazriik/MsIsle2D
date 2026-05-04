using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    private bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            paused = true;
            Debug.Log("Paused Game");

            
        }
        else
        {
            paused = false;
            Debug.Log("Unpaused Game");
        }
    }

    
}
