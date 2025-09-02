using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlots;
    // see InventoryObject class implementation in InventoryObject.cs
    [SerializeField] private List<InventoryObject> objects;
    public InventoryObject equippedObject = null;
    private int equippedIdx = -1;
    public GameObject equippedObj;
    public GameObject toolRefPoint;
    public float tolerance = 0.1f;
    public float pullForce = 10f;

    public void AddObject(InventoryObject invObject)
    {
        if (objects.Count == inventorySlots.Count) return;
        Image slotImage = inventorySlots[objects.Count].GetComponent<Image>();
        slotImage.sprite = invObject.icon;
        slotImage.color = new Color(1f, 1f, 1f, 1f);
        objects.Add(invObject);
    }

    void Equip(int idx)
    {
        if (idx >= objects.Count) return;
        Debug.Log("equipping " + idx);
        if (equippedIdx > -1)
        {
            inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            objects[equippedIdx].obj.SetActive(false);
        }
        inventorySlots[idx].GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
        equippedObject = objects[idx];
        equippedIdx = idx;
        equippedObject.obj.SetActive(true);
        equippedObject.obj.transform.position = toolRefPoint.transform.position;

        NoCollision();
    }

    void NoCollision()
    {
        equippedObject.obj.layer = LayerMask.NameToLayer("NoCollision");

        if (equippedObject.obj.transform.childCount == 0) return;
        foreach (Transform child in equippedObject.obj.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("NoCollision");
        }
    }

    void Unequip()
    {
        inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        objects[equippedIdx].obj.SetActive(false);
        equippedObject = null;
        equippedIdx = -1;
    }

    public void RemoveObject()
    {

    }

    void Update()
    {
        for (int i = 0; i <= 4; i++)
        {
            if (equippedIdx != i && Input.GetKey(KeyCode.Alpha1 + i))
            {
                Equip(i);
            }
        }

        if (equippedIdx > -1 && Input.GetKey(KeyCode.Q))
        {
            Unequip();
        }

        // handle holding of tool/object
        if (equippedIdx > -1)
        {
            equippedObj = equippedObject.obj;
            EquippedLocation();
        }
    }

    void EquippedLocation()
    {
        if (Vector3.Distance(equippedObj.transform.position, toolRefPoint.transform.position) > tolerance)
        {
            equippedObj.GetComponent<Rigidbody>().velocity = (toolRefPoint.transform.position - equippedObj.transform.position) * Vector3.Distance(equippedObj.transform.position, toolRefPoint.transform.position) * pullForce;
        }
        else
        {
            equippedObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
