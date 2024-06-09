using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Index of the current scene
    public string sceneName;
    public KeyCode keycode;
    void Start()
    {
       
    }

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(keycode))
        {
            ChangeScene(sceneName);
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
