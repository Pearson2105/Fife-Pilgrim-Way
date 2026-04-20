using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        LevelManager.Instance.LoadScene("0.5 priest intro", "CrossFade");
    }

    public void Quit()
    {
        Application.Quit();
    }
}