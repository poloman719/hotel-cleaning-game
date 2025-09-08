using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Curtains : MonoBehaviour, IInteractable
{
    public GameObject openCurtains;
    public GameObject closedCurtains;
    public string interactTextOpen = "Open Curtains (F)";
    public string interactTextClose = "Close Curtains (F)";

    public void Interact()
    {
        if (!openCurtains.activeInHierarchy)
        {
            openCurtains.SetActive(true);
            closedCurtains.SetActive(false);
        }

        else
        {
            closedCurtains.SetActive(true);
            openCurtains.SetActive(false);
        }
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
        if (!openCurtains.activeInHierarchy)
        {
            displayedText.text = interactTextOpen;
        }

        else
        {
            displayedText.text = interactTextClose;
        }
    }
}
