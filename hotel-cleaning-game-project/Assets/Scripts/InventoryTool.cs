using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Makes it visible in Inspector
public class InventoryTool
{
    public string toolType;
    public GameObject toolObject;
    public Sprite icon;

    public InventoryTool(string tt, GameObject to, Sprite i)
    {
        toolType = tt;
        toolObject = to;
        icon = i;
    }
}
