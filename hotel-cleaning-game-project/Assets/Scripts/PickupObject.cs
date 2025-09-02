using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupObject : MonoBehaviour
{
    public string objectType;
    public string displayText;
    public Sprite objectIcon;
    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;
    private TextMeshProUGUI text;

    bool inInventory = false;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        raycastManager = FindObjectOfType<RaycastManager>();
        text = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
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

    bool IsChildOfThis(GameObject obj)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject == obj) 
                return true; 
        }

        return false;
    }
}
