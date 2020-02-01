using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelector : MonoBehaviour
{
    private bool enabled = false;
    private bool musicSelected = false;

    private int controllerNumber;
    private float inputDelay = 0f;

    public Color playerColor;

    public MusicSelection[] musicSelections;
    private int currentSelection;

    public int numMusicCols;
    public int numMusicRows;

    // Start is called before the first frame update
    void Awake()
    {
        transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (!musicSelected)
            {
                if (inputDelay <= 0f)
                {
                    MoveCursorHorizontal(Input.GetAxis("Horizontal" + controllerNumber));
                    MoveCursorVertical(Input.GetAxis("Vertical" + controllerNumber));
                    transform.position = musicSelections[currentSelection].transform.position;
                }
                else
                {
                    inputDelay -= Time.deltaTime;
                }

                if (Input.GetButtonDown("Select" + controllerNumber) && !musicSelections[currentSelection].selected)
                {
                    musicSelected = true;
                    musicSelections[currentSelection].selected = true;
                    musicSelections[currentSelection].GetComponent<Image>().enabled = true;
                    musicSelections[currentSelection].GetComponent<Image>().color = playerColor;
                }
            }
            else
            {
                if(Input.GetButtonDown("Cancel" + controllerNumber))
                {
                    musicSelected = false;
                    musicSelections[currentSelection].selected = false;
                    musicSelections[currentSelection].GetComponent<Image>().enabled = false;
                }
            }
        }
    }

    public void EnableCursor(int controllerIndex)
    {
        enabled = true;
        controllerNumber = controllerIndex;
        transform.GetChild(0).GetComponent<Image>().enabled = true;
        currentSelection = 0;
        transform.position = musicSelections[0].transform.position;
    }

    void MoveCursorHorizontal(float horizontal)
    {
        if(horizontal > 0.5f)
        {
            if ((currentSelection + 1) % numMusicCols == 0)
            {
                currentSelection -= (numMusicCols-1);
            }
            else
            {
                currentSelection++;
            }
            inputDelay = 0.2f;
        }
        else if(horizontal < -0.5f)
        {
            if (currentSelection % numMusicCols == 0)
            {
                currentSelection += (numMusicCols - 1);
            }
            else
            {
                currentSelection--;
            }
            inputDelay = 0.2f;
        }
    }
    void MoveCursorVertical(float vertical)
    {
        if (vertical > 0.5f)
        {
            if ((currentSelection + numMusicCols) > musicSelections.Length-1)
            {
                currentSelection -= numMusicCols;
            }
            else
            {
                currentSelection += numMusicCols;
            }
            inputDelay = 0.2f;
        }
        else if (vertical < -0.5f)
        {
            if((currentSelection - numMusicCols) < 0)
            {
                currentSelection += numMusicCols * (numMusicRows-1);
            }
            else
            {
                currentSelection -= numMusicCols;
            }
            inputDelay = 0.2f;
        }
    }

    public int GetCurrentSelection()
    {
        return currentSelection;
    }

    public bool GetReady()
    {
        return musicSelected;
    }
}
