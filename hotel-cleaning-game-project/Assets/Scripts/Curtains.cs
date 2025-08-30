using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour, IInteractable
{
    public GameObject openCurtains;
    public GameObject closedCurtains;

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
}
