using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Makes it visible in Inspector
public class InventoryObject
{
    public string objectType;
    public GameObject obj;
    public Sprite icon;

    public InventoryObject(string ot, GameObject go, Sprite i)
    {
        objectType = ot;
        obj = go;
        icon = i;
    }
}
