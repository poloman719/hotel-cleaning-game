using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedText : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void ResetFlash()
    {
        anim.SetBool("flash", false);
    }
}
