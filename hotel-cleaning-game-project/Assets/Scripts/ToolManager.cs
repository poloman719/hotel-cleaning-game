using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    public string toolType = "";
    public Sprite toolIcon;
    // private bool inToolbar = false;
    // private bool equipped = false;

    void Start()
    {

    }

    public void AddToInventory(PlayerInventory inv)
    {
        inv.AddObject(new InventoryTool(toolType, this.gameObject, toolIcon));
        // inToolbar = true;
        gameObject.SetActive(false);
    }

    void Update()
    {

    }
}
