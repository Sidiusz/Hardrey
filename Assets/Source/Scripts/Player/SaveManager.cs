using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public string saveSceneName;
    public Vector3 savePosition;
    public GameObject deathPanel;
    public PlayerController playerController;

    public void SaveGame()
    {
        // Save current scene name and player position
        saveSceneName = SceneManager.GetActiveScene().name;
        savePosition = transform.position;
    }

    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().name == saveSceneName)
        {
            // Teleport player to save position
            transform.position = savePosition;
        }
        else
        {
            // Load save scene and teleport player to save position
            SceneManager.LoadScene(saveSceneName);
            transform.position = savePosition;
        }

        // Enable player control
        playerController.controlDisabled = false;

        // Hide death panel
        deathPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DangerousObject")
        {
            // Disable player control
            playerController.controlDisabled = true;

            // Show death panel
            deathPanel.SetActive(true);
        }
        else if (other.tag == "SavePoint")
        {
            // Save current scene name and player position
            SaveGame();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }
}
