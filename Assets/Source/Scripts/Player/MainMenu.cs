using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject loadingScreen;
    public AudioSource loadingSound;
    public float fadeDuration = 1f;

private CanvasGroup canvasGroup;
    private bool isLoading = false;

    private void Start()
    {
        canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (isLoading)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, fadeDuration * Time.deltaTime);
            if (canvasGroup.alpha == 1f)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void StartGame()
    {
        loadingScreen.SetActive(true);
        loadingSound.Play();
        isLoading = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}