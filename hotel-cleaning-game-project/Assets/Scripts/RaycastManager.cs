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
    private TextMeshProUGUI pickUpTextUI;
    private TextMeshProUGUI interactTextUI;

    void Start()
    {
        reticleAnim = GameObject.Find("Reticle").GetComponent<Animator>();
        pickUpTextUI = GameObject.Find("PickupText").GetComponent<TextMeshProUGUI>();
        interactTextUI = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
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

            if (objectDetected.TryGetComponent<IInteractable>(out IInteractable interactable)) //If object is interactable
            {
                activeReticle = true;

                if (objectDetected.TryGetComponent<PickupObject>(out PickupObject pickUpObject) && pickUpObject.CanBePickedUp()) //also pickable
                {
                    pickUpTextUI.color = new Color(1f, 1f, 1f, 1f);
                    pickUpTextUI.text = pickUpObject.displayText;
                    interactable.DisplayInteractText(interactTextUI);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickUpObject.Pickup();
                        pickUpTextUI.text = "";
                    }
                }
                else
                {
                    interactable.DisplayInteractText(pickUpTextUI);
                }

                if (Input.GetKeyDown(KeyCode.F))
                    interactable.Interact();
            }

            else if (objectDetected.CompareTag("Item") || objectDetected.CompareTag("Tool")) //If object is touchable & not interactable
            {
                activeReticle = true;
                interactTextUI.text = "";
                PickupObject pickUpObject = objectDetected.GetComponentInParent<PickupObject>(); //allows objects with no colliders in parent but colliders in children to be picked up

                if (pickUpObject != null && pickUpObject.CanBePickedUp()) //If object is pickable
                {
                    pickUpTextUI.color = new Color(1f, 1f, 1f, 1f);
                    pickUpTextUI.text = pickUpObject.displayText;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickUpObject.Pickup();
                        pickUpTextUI.text = "";
                    }
                }
            }

            else 
            {
                //For when ray hits something unrelevant
                pickUpTextUI.text = "";
                interactTextUI.text = "";
            }
        }
        else
        {
            //For when ray hits nothing
            detecting = false;
            objectDetected = null;
            pickUpTextUI.text = "";
            interactTextUI.text = "";
        }

        reticleAnim.SetBool("detecting", activeReticle);
    }
}
