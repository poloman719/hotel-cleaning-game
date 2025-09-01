using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// NOTE: This script might need to be attached to all pickable objects and renamed to "PickupObject"
public class PickupTool : MonoBehaviour
{
    public string toolType;
    public string displayText;
    public Sprite toolIcon;
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
            InventoryTool newTool = new InventoryTool(toolType, gameObject, toolIcon);
            playerInventory.AddObject(newTool);
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
