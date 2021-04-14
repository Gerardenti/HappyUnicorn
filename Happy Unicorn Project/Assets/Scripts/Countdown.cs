using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    Text time;
    public static float timeLeft = 90.0f;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 90f;
        time = GetComponent<Text>();
    }

    // Update is called once per frame


    void Update()
    {
        float delta = Time.deltaTime * 1000;
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
            SceneManager.LoadScene("TimeOut");
        }
        time.text = "Time: " + Mathf.Round(timeLeft);
    }
}
