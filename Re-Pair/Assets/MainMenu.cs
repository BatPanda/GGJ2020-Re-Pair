using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Camera.main);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < 7; i++)
        {
            if(Input.GetButtonDown("Start" + i))
            {
                SceneManager.LoadScene(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
