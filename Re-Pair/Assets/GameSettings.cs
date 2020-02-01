using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerSettings
    {
        [HideInInspector]
        public int playerNum;

        public bool connected;
        public int musicSelected;
    }

    public DetectController[] detectControllers;

    public PlayerSettings[] playerSettings = new PlayerSettings[4];
    private List<int> detectedControllers = new List<int>();
    private int controllersConnected = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i<playerSettings.Length; i++)
        {
            playerSettings[i].playerNum = i;
        }
    }

    private void Update()
    {
        for(int i = 1; i<7; i++)
        {
            if(Input.GetButtonDown("Start" + i) && !detectedControllers.Contains(i))
            {
                detectedControllers.Add(i);
                playerSettings[controllersConnected].connected = true;
                playerSettings[controllersConnected].playerNum = i;
                detectControllers[controllersConnected++].AddPlayer(i);
            }
        }

        if (detectedControllers.Count > 0)
        {
            if (Input.GetButtonDown("Fire" + detectedControllers[0]))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
