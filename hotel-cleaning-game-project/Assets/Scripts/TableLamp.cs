using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLamp : MonoBehaviour, IInteractable
{
    public GameObject lampLight;
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
        lampLight.SetActive(true);
    }

    void TurnOff()
    {
        lampLight.SetActive(false);
    }
}
