using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTool : MonoBehaviour
{
    public string toolType;
    public Sprite toolIcon;
    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        raycastManager = FindObjectOfType<RaycastManager>();
    }

    void Update()
    {
        if ((raycastManager.objectDetected == gameObject || IsChildOfThis(raycastManager.objectDetected)) && Input.GetKeyDown(KeyCode.E))
        {
            InventoryTool newTool = new InventoryTool(toolType, gameObject, toolIcon);
            playerInventory.AddObject(newTool);
            gameObject.SetActive(false);
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
