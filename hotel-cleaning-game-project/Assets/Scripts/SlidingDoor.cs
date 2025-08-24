using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour, IInteractable
{
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
