using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Slider progressBar;
    public GameObject transitionsContainer;

    [Header("Polish Settings")]
    public float minLoadScreenTime = 0.5f;
    public float blackScreenHoldTime = 0.1f;

    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
        progressBar.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

        if (transition == null)
        {
            Debug.LogError("Transition not found: " + transitionName);
            yield break;
        }

        yield return transition.AnimateTransitionIn();

        yield return new WaitForSeconds(blackScreenHoldTime);

 
        progressBar.gameObject.SetActive(true);
        progressBar.value = 0f;

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        float startTime = Time.time;

        while (scene.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(scene.progress / 0.9f);
            progressBar.value = Mathf.Lerp(progressBar.value, progress, Time.deltaTime * 8f);

            yield return null;
        }

        while (progressBar.value < 1f)
        {
            progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime * 2f);
            yield return null;
        }

        float elapsed = Time.time - startTime;
        if (elapsed < minLoadScreenTime)
        {
            yield return new WaitForSeconds(minLoadScreenTime - elapsed);
        }

        progressBar.value = 1f;

        yield return new WaitForSeconds(0.15f);

        scene.allowSceneActivation = true;

        while (!scene.isDone)
            yield return null;

        progressBar.gameObject.SetActive(false);

        yield return transition.AnimateTransitionOut();
    }
}