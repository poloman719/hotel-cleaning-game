using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    // variables
    // Camera
    public GameObject mainCam;
    public float maxDist;
    public GameObject objectDetected = null;

    private Animator reticleAnim;

    void Start()
    {
        reticleAnim = GameObject.Find("Reticle").GetComponent<Animator>();
    }

    void Update()
    {
        // Create Ray
        Ray interactRay = new Ray(mainCam.transform.position, mainCam.transform.forward);
        //Debug line
        Debug.DrawRay(interactRay.origin, interactRay.direction * maxDist, Color.red);

        // Detect Item
        RaycastHit itemDetected;
        if (Physics.Raycast(interactRay, out itemDetected, maxDist))
        {
            objectDetected = itemDetected.collider.gameObject;

            if (objectDetected.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                reticleAnim.SetBool("detectInteractable", true);
                if (Input.GetMouseButtonDown(0))
                    interactable.Interact();
            }

            else
                reticleAnim.SetBool("detectInteractable", false);
        }
    }
}

// Step 1: Draw Line
// Step 2:
