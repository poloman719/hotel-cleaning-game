using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLamp : MonoBehaviour, IInteractable
{
    public List<GameObject> lampLights;
    bool switchState = false;

    public void Interact()
    {
        switchState = !switchState;

        if (switchState)
            TurnOn();
        else
            TurnOff();
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
