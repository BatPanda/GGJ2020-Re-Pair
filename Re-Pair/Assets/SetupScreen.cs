using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupScreen : MonoBehaviour
{
    public GameSettings gameSettings;


    public DetectController[] detectControllers;
    public MusicSelector[] musicSelectors;

    public GameObject musicSelectionGroup;

    private List<int> detectedControllers = new List<int>();
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
            if (Input.GetButtonDown("Fire" + detectedControllers[0]))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
