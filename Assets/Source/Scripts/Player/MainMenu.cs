using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public Image fadingScreen;
    public GameObject loadingScreen;
    public AudioClip startButtonSound;
    public AudioClip exitButtonSound;
    public AudioSource audioSource;
    public float fadeDuration = 1f;
    public float alphacol = 1f;

private bool isLoading = false;

    private void Start()
    {
        fadingScreen.color = new Color(0f, 0f, 0f, alphacol);
        loadingScreen.SetActive(false);
    }

    private void Update()
    {
        if (isLoading)
        {
            fadingScreen.color = Color.Lerp(fadingScreen.color, Color.black, fadeDuration * Time.deltaTime);
            if (fadingScreen.color.a == 1f)
            {
                loadingScreen.SetActive(true);
                StartCoroutine(FakeLoad());
            }
        }
    }

    IEnumerator FakeLoad()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    public void StartGame()
    {
        fadingScreen.gameObject.SetActive(true);
        audioSource.PlayOneShot(startButtonSound);
        isLoading = true;
    }

    public void ExitGame()
    {
        fadingScreen.gameObject.SetActive(true);
        audioSource.PlayOneShot(exitButtonSound);
        isLoading = true;
    }
}