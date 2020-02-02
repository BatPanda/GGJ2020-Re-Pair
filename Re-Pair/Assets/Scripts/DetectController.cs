using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectController : MonoBehaviour
{
    [Range(1,4)]
    public int controllerNum;

    public GameSettings gameSettings;


    [SerializeField]
    private bool controllerConnected = false;


    private void Awake()
    {

        if (!gameSettings)
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }
    }

    private void Update()
    {

    }
    
    public void AddPlayer(int controllerID)
    {
        controllerNum = controllerID;
        GetComponentInChildren<Image>().enabled = true;
    }
}
