using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenLoader : MonoBehaviour
{
    // The name of the scene to load after the splash screen
    [SerializeField] private string sceneToLoad = "MainScene";
    // The duration of the splash screen in seconds
    [SerializeField] private float splashDuration = 6.0f;

    private void Start()
    {
        // Start the coroutine to load the next scene after a delay
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(splashDuration);
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
