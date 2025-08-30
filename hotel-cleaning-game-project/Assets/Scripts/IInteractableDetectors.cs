using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableDetectors : MonoBehaviour, IInteractable
{
    public GameObject parentInteractable;
    public string scriptName;

    public void Interact()
    {
        Type componentType = Type.GetType(scriptName);
        var interactMethod = componentType.GetMethod("Interact");

        Component parentScript = parentInteractable.GetComponent(componentType);

        interactMethod.Invoke(parentScript, null);
    }
}
