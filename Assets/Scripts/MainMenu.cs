using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("0.5 priest intro");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
