using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingDoor : MonoBehaviour, IInteractable
{
    public string interactTextOpen = "Open Shower Door (F)";
    public string interactTextClose = "Close Shower Door (F)";

    bool switchState = false;
    Animator anim;

    bool ready = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!ready)
            return;

        switchState = !switchState;

        if (switchState)
            Open();
        else
            Close();
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
        if (switchState)
            displayedText.text = interactTextClose;
        else
            displayedText.text = interactTextOpen;
    }

    public void Ready()
    {
        ready = true;
    }

    void Open()
    {
        ready = false;
        anim.SetBool("Opening", true);
    }

    void Close()
    {
        ready = false;
        anim.SetBool("Opening", false);
    }
}
