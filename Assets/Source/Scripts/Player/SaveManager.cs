using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public string saveSceneName;
    public Vector3 savePosition;
    public GameObject deathPanel;
    public GameObject savePanel;
    public PlayerController playerController;
    public TMP_Text deathCountText;

    private int deathCount;

    public void SaveGame()
    {
        saveSceneName = SceneManager.GetActiveScene().name;
        savePosition = transform.position;
    }

    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().name == saveSceneName)
        {
            transform.position = savePosition;
        }
        else
        {
            SceneManager.LoadScene(saveSceneName);
            transform.position = savePosition;
        }

        playerController.controlDisabled = false;
        deathPanel.SetActive(false);

        deathCount = PlayerPrefs.GetInt("deathCount", 0);
        deathCountText.text = deathCount.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DangerousObject")
        {
            playerController.controlDisabled = true;
            deathPanel.SetActive(true);
            deathCount++;
            deathCountText.text = deathCount.ToString();
            PlayerPrefs.SetInt("deathCount", deathCount);
        }
        else if (other.tag == "SavePoint")
        {
            SaveGame();
            savePanel.SetActive(true);
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
