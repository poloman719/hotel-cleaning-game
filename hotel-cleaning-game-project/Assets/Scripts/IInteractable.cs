using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IInteractable
{
    void Interact();
    void DisplayInteractText(TextMeshProUGUI displayedText);
}
