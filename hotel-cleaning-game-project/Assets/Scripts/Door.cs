using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    bool switchState = false;
    Animator anim;

    bool ready = true;

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
