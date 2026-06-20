using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Slider progressBar;
    private Canvas canvas;
    public GameObject transitionsContainer;
    private SceneTransition[] transitions;

    private void Awake()
    {
        // Singleton. If there is no instance of this object in the scene, make one.
        // If there's already one there, Destroy. If there isn't, create one.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get the SceneTransition script from children.
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
        canvas = GetComponent<Canvas>();
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        // Start LoadScene Coroutine.
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    // Coroutine that manages Scene Transitions with Loading Bar and Scene Transition Animations.
    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        // Move UI to Sorting Order 100 so it's on top of everything.
        canvas.sortingOrder = 100;
        // For Loop. But using System.linq to have a "one-liner". First time using it to see how it is.
        // Looping through the "transitions" array until we find the "First" element called t that satisfies the following condition:
        // Where the name of that transition "t" is equal to the transition name that we passed as the parameter when we call this method.
        // We will get this scene and store it in a temporary scene transition variable.
        SceneTransition transition = transitions.First(t => t.name == transitionName);

        // Load scene that was passed in as a string asynchronous. (Run in parallel)
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        // Turn off Auto Scene Activation after load so we can play the Scene Transition Animation.
        scene.allowSceneActivation = false;
        // Yield/Wait until the animation is done.
        yield return transition.AnimateTransitionIN();
        // Set the Loading Bar ACTIVE.
        progressBar.gameObject.SetActive(true);

        do
        {
            // Set the scene load progress as the value of the Loading Bar.
            progressBar.value = scene.progress;
            yield return null;
            // Do this while the progress bar is < 90% filled.
        } while (scene.progress < 0.9f);

        // Then we activate Acene Activation again so we load into the scene.
        scene.allowSceneActivation = true;
        // Turn off the Loading Bar.
        progressBar.gameObject.SetActive(false);
        // Yield/Wait until animation is done.
        yield return transition.AnimateTransitionOUT();
        // Move UI to Sorting Order -1 so it's behind the Scene Transitions.
        canvas.sortingOrder = -1;
    }
}
