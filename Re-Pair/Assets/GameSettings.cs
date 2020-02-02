using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerSettings
    {
        public int playerNum;

        public bool connected;
        public int musicSelected;

        public bool alive;

        public Color playerColor;
    }
    public PlayerSettings[] playerSettings = new PlayerSettings[4];


    private void Awake()
    {
        if (FindObjectsOfType<GameSettings>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {

    }

    public int FindPlayerNumberByController(int controller)
    {
        for(int i = 0; i < playerSettings.Length; i++)
        { 
            if(playerSettings[i].playerNum == controller)
            {
                return i;
            }
        }
        return -1;
    }
}
