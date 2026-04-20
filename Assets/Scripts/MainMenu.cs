using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log(LevelManager.Instance);

        LevelManager.Instance.LoadScene("0.5 priest intro", "CrossFade");
    }

    public void Quit()
    {
        Application.Quit();
    }
}