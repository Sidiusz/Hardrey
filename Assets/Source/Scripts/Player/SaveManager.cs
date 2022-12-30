using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public string saveSceneName;
    public Vector3 savePosition;
    public GameObject deathPanel;
    public GameObject savePanel;
    public Animator savePanelAnimator;
    public PlayerController playerController;
    public TMP_Text deathCountText;

    private int deathCount;
    public string saveFileName = "save.txt";
    private string savePath;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/" + saveFileName;
        LoadGame();
        Debug.Log(Application.persistentDataPath + "/save.txt");
    }

    public void SaveGame()
    {
        saveSceneName = SceneManager.GetActiveScene().name;
        savePosition = transform.position;
        deathCount = PlayerPrefs.GetInt("deathCount", 0);
        deathCountText.text = deathCount.ToString();

        string saveData = JsonUtility.ToJson(this, true);
        File.WriteAllText(savePath, saveData);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath)) { Debug.LogError("Save file not found at " + savePath); return; }

        string saveData = File.ReadAllText(savePath);
        JsonUtility.FromJsonOverwrite(saveData, this);

        if (SceneManager.GetActiveScene().name == saveSceneName) {
            transform.position = savePosition; }
        else {
            SceneManager.LoadScene(saveSceneName);
            transform.position = savePosition; }

        playerController.controlDisabled = false;
        deathPanel.SetActive(false); }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DangerousObject")
        {
            playerController.controlDisabled = true;
            deathPanel.SetActive(true);
            deathCount++;
            deathCountText.text = deathCount.ToString();
        }
        else if (other.tag == "SavePoint")
        {
            SaveGame();
            savePanelAnimator.SetTrigger("Show");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadGame();
        }
    }
}