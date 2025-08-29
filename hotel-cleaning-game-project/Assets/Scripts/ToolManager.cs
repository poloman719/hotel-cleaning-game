using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private RaycastManager rm;
    private PlayerInventory inv;
    public string toolTag = "Tool";
    public GameObject InventoryObj;
    private bool addingTool = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rm = GetComponent<RaycastManager>();
        inv = InventoryObj.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!addingTool && Input.GetMouseButtonDown(0) && rm.objectDetected && rm.objectDetected.CompareTag(toolTag))
        {
            Debug.Log("tool detected");
            Tool tool = rm.objectDetected.GetComponent<Tool>();
            Debug.Log(tool);
            tool.AddToInventory(inv);
            addingTool = true;
        }
        if (addingTool && !Input.GetMouseButtonDown(0))
        {
            addingTool = false;
        }
    }
}
