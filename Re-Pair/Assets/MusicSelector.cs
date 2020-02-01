using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelector : MonoBehaviour
{
    private bool enabled = false;

    private int controllerNumber;
    private float inputDelay = 0f;

    public MusicSelection[] musicSelections;
    private int currentSelection;

    public int numMusicCols;
    public int numMusicRows;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponentInChildren<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDelay <= 0f && enabled)
        {
            MoveCursorHorizontal(Input.GetAxis("Horizontal" + controllerNumber));
            MoveCursorVertical(Input.GetAxis("Vertical" + controllerNumber));
        }
        else
        {
            inputDelay -= Time.deltaTime;
        }
    }

    public void EnableCursor(int controllerIndex)
    {
        enabled = true;
        controllerNumber = controllerIndex;
        GetComponentInChildren<Image>().enabled = true;
        currentSelection = 0;
        transform.position = musicSelections[0].transform.position;
    }

    void MoveCursorHorizontal(float horizontal)
    {
        if (horizontal != 0)
        {
            if(horizontal > 0)
            {
                if ((currentSelection + 1) % numMusicCols == 0)
                {
                    currentSelection -= (numMusicCols-1);
                }
                else
                {
                    currentSelection++;
                }
            }
            
            inputDelay = 0.2f;
        }
    }
    void MoveCursorVertical(float vertical)
    {
        if (vertical < 0)
        {
            if ((currentSelection + numMusicCols) > musicSelections.Length-1)
            {
                currentSelection -= numMusicCols;
            }
            else
            {
                currentSelection += numMusicCols;
            }
            Debug.Log(currentSelection);
        }
        inputDelay = 0.2f;
    }
}
