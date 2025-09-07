using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupObject : MonoBehaviour //this script is to be attached to all pickable items and tools
{
    public string objectType;
    public string displayText;
    public Sprite objectIcon;
    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;
    private TextMeshProUGUI text;

    public int objLayer;
    [HideInInspector] public bool beingHeld = false;
    [HideInInspector] public bool inInventory = false;
    bool isTouchingPlayer = false;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        raycastManager = FindObjectOfType<RaycastManager>();
        text = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        objLayer = gameObject.layer;
    }

    void Update()
    {
        if (inInventory) return;
        if (!raycastManager.detecting) return;
        if (raycastManager.objectDetected != gameObject && !IsChildOfThis(raycastManager.objectDetected)) return;

        text.text = displayText;

        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryObject newObject = new InventoryObject(objectType, gameObject, objectIcon);
            playerInventory.AddObject(newObject);
            gameObject.SetActive(false);
            text.text = "";
            inInventory = true;
        }
    }

    void FixedUpdate() //Runs before OnTriggerStay and OnTriggerExit
    {
        if (!isTouchingPlayer && !beingHeld)
        {
            gameObject.layer = objLayer;
        }

        isTouchingPlayer = false;
    }

    bool IsChildOfThis(GameObject obj)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject == obj) 
                return true; 
        }

        return false;
    }

    public void BeingHeld(bool isHeld) //Only for items
    {
        beingHeld = isHeld;
        if (isHeld) gameObject.layer = LayerMask.NameToLayer("Holding"); //no else statement because reset layer happens in OnTriggerExit
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && beingHeld == false)
        {
            gameObject.layer = objLayer;
        }
    }
}
