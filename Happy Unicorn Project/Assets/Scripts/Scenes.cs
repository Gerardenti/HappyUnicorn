using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes: MonoBehaviour
{
    private KeyCode startButton = KeyCode.F;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(startButton))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Title") {
                SceneManager.LoadScene("SampleScene");
            } else if (scene.name == "GameOver") {
                SceneManager.LoadScene("Title");
            } else if (scene.name == "Victory") {
                SceneManager.LoadScene("Title");
            } else if (scene.name == "TimeOut") {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
