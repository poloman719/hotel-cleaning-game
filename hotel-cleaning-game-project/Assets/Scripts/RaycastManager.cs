using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaycastManager : MonoBehaviour
{
    public GameObject mainCam;
    public float maxDist;
    public LayerMask layermask;
    public GameObject objectDetected = null;
    public bool detecting = false;

    private Animator reticleAnim;
    private TextMeshProUGUI text;

    void Start()
    {
        reticleAnim = GameObject.Find("Reticle").GetComponent<Animator>();
        text = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Create Ray
        Ray interactRay = new Ray(mainCam.transform.position, mainCam.transform.forward);
        //Debug line
        Debug.DrawRay(interactRay.origin, interactRay.direction * maxDist, Color.red);

        // Detect Item
        RaycastHit itemDetected;
        bool activeReticle = false;

        if (Physics.Raycast(interactRay, out itemDetected, maxDist, layermask))
        {
            detecting = true;
            objectDetected = itemDetected.collider.gameObject;

            if (objectDetected.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                activeReticle = true;
                if (Input.GetKeyDown(KeyCode.F))
                    interactable.Interact();
            }
            else if (objectDetected.CompareTag("Item") || objectDetected.CompareTag("Tool"))
            {
                activeReticle = true;
            }
            else
            {
                //For when ray hits something unrelevant
                text.text = "";
            }
        }
        else
        {
            //For when ray hits nothing
            detecting = false;
            text.text = "";
        }

        reticleAnim.SetBool("detecting", activeReticle);
    }
}
