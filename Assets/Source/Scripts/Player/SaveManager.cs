using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public string saveFileName;
    public GameObject deathPanel;
    public GameObject savePanel;
    public TMP_Text deathCountText;

    private int deathCount;
    private Vector3 savedPosition;

    private void Start()
    {
        LoadGame();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SavePoint"))
        {
            savedPosition = transform.position;
            SaveGame();
            savePanel.GetComponent<Animator>().SetTrigger("Show");
        }
        else if (other.CompareTag("DangerZone"))
        {
            deathPanel.SetActive(true);
            GetComponent<PlayerController>().controlDisabled = true;
            deathCount++;
            SaveGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadGame();
        }
    }

    private void SaveGame()
    {
        string saveData = JsonUtility.ToJson(new SaveData(savedPosition, deathCount));
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, saveData);
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            string saveData = File.ReadAllText(Application.persistentDataPath + "/" + saveFileName);
            SaveData data = JsonUtility.FromJson<SaveData>(saveData);
            savedPosition = data.savedPosition;
            deathCount = data.deathCount;
            deathCountText.text = deathCount.ToString();
            transform.position = savedPosition;
            GetComponent<PlayerController>().controlDisabled = false;
            deathPanel.SetActive(false);
        }
    }
}

[System.Serializable]
class SaveData
{
    public Vector3 savedPosition;
    public int deathCount;

    public SaveData(Vector3 savedPosition, int deathCount)
    {
        this.savedPosition = savedPosition;
        this.deathCount = deathCount;
    }
}