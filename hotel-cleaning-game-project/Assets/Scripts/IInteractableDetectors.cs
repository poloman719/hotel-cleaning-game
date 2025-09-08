using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IInteractableDetectors : MonoBehaviour, IInteractable
{
    public GameObject parentInteractable;
    public string scriptName;

    private Type componentType;
    private Component parentScript;

    void Start ()
    {
        componentType = Type.GetType(scriptName); 
        parentScript = parentInteractable.GetComponent(componentType);
    }

    public void Interact()
    {
        var interactMethod = componentType.GetMethod("Interact");
        interactMethod.Invoke(parentScript, null);
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
        var displayInteractTextMethod = componentType.GetMethod("DisplayInteractText"); 
        displayInteractTextMethod.Invoke(parentScript, new object[] { displayedText });
    }
}
