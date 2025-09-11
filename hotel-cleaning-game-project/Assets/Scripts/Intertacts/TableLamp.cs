using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableLamp : MonoBehaviour, IInteractable
{
    public List<GameObject> lampLights;
    public string interactTextOpen = "Turn On Lamp (F)";
    public string interactTextClose = "Turn Off Lamp (F)";

    bool switchState = false;

    public void Interact()
    {
        switchState = !switchState;

        if (switchState)
            TurnOn();
        else
            TurnOff();
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
        if (switchState)
            displayedText.text = interactTextClose;
        else
            displayedText.text = interactTextOpen;
    }

    void TurnOn()
    {
        for (int i = 0; i < lampLights.Count; i++)
        {
            lampLights[i].SetActive(true);
        }
    }

    void TurnOff()
    {
        for (int i = 0; i < lampLights.Count; i++)
        {
            lampLights[i].SetActive(false);
        }
    }
}
