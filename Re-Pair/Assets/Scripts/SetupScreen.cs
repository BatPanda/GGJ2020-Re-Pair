using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScreen : MonoBehaviour
{
    public GameSettings gameSettings;
    public int gameScene = 1;

    public DetectController[] detectControllers;
    public MusicSelector[] musicSelectors;

    public GameObject musicSelectionGroup;

    private List<int> detectedControllers = new List<int>();
    private bool[] readyPlayers = new bool[4];
    private int controllersConnected = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (!gameSettings)
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        MusicSelection[] musicSelections = musicSelectionGroup.GetComponentsInChildren<MusicSelection>();
        for(int i = 0; i<musicSelections.Length; i++)
        {
            musicSelections[i].musicIndex = i;
            musicSelections[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
        }

        foreach(MusicSelector musicSelector in musicSelectors)
        {
            musicSelector.musicSelections = musicSelections;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 7; i++)
        {
            if (Input.GetButtonDown("Start" + i) && !detectedControllers.Contains(i))
            {
                detectedControllers.Add(i);
                gameSettings.playerSettings[controllersConnected].connected = true;
                gameSettings.playerSettings[controllersConnected].playerNum = i;
                detectControllers[controllersConnected].AddPlayer(i);
                musicSelectors[controllersConnected].EnableCursor(i);
                controllersConnected++;
            }
        }

        if (detectedControllers.Count > 0)
        {
            for (int i = 0; i < detectedControllers.Count; i++)
            {
                if (Input.GetButtonDown("Select" + detectedControllers[i]))
                {
                    gameSettings.playerSettings[i].musicSelected = musicSelectors[i].GetCurrentSelection();
                    readyPlayers[i] = true;
                }
                if (Input.GetButtonDown("Cancel" + detectedControllers[i]))
                {
                    gameSettings.playerSettings[i].musicSelected = -1;
                    readyPlayers[i] = false;
                }
                if(Input.GetButtonDown("Fire" + detectedControllers[i]) && detectedControllers.Count > 1)
                {
                    bool allReady = true;
                    for (int j = 0; j < detectedControllers.Count; j++)
                    {
                        if(!musicSelectors[j].GetReady())
                        {
                            allReady = false;
                        }
                    }
                    if(allReady)
                    {
                        SceneManager.LoadScene(gameScene);
                    }
                }
            }
        }
    }
}
