using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string interactTextOpen = "Open Door (F)";
    public string interactTextClose = "Close Door (F)";

    bool switchState = false;
    Animator anim;

    bool ready = true;
    bool roomDoor = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (gameObject.name.Contains("RoomDoor"))
            roomDoor = true;
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
        anim.SetBool("opening", true);
    }

    void Close()
    {
        ready = false;
        anim.SetBool("opening", false);
    }
}
