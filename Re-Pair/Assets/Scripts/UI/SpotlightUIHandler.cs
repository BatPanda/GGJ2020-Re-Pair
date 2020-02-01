using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotlightUIHandler : MonoBehaviour
{
    public Image spotlightTimer;
    ChooseCharacterScript spotlightScript;

    private void Awake()
    {
        spotlightScript = FindObjectOfType<ChooseCharacterScript>();
    }

    private void Update()
    {
        if (spotlightScript.timeIsStopped)
        {
            spotlightTimer.enabled = true;
            spotlightTimer.fillAmount = (spotlightScript.startFreezeTime - spotlightScript.freezeTime) / spotlightScript.startFreezeTime;
        }
        else
        {
            spotlightTimer.enabled = false;
        }
    }
}
