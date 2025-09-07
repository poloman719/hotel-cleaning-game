using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlotsBG;
    [SerializeField] private List<GameObject> inventorySlots;
    [System.NonSerialized] public InventoryObject[] objects;

    public InventoryObject equippedObject = null;
    [SerializeField] private int equippedIdx = -1;
    public GameObject equippedObj;
    public PickupObject pickupObject = null;

    public GameObject toolRefPoint;
    public float tolerance = 0.1f;
    public float pullForce = 10f;

    void Awake()
    {
        objects = new InventoryObject[5];
    }

    public void AddObject(InventoryObject invObject)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null)
            {
                objects[i] = invObject;
                Image slotImage = inventorySlots[i].GetComponent<Image>();
                slotImage.sprite = invObject.icon;
                slotImage.color = new Color(1f, 1f, 1f, 1f);
                return;
            }
        }

        Debug.Log("Inventory full");
    }

    void Equip(int idx)
    {
        if (idx < 0 || idx >= inventorySlotsBG.Count) return;
        if (equippedIdx == idx)
        {
            Unequip();
            return;
        }

        if (equippedIdx > -1 && equippedIdx < inventorySlotsBG.Count)
        {
            inventorySlotsBG[equippedIdx].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);

            //equippedIdx is in bounds check
            if (equippedObj != null && equippedIdx < objects.Length && objects[equippedIdx].obj != null)
                objects[equippedIdx].obj.SetActive(false);
        }

        equippedIdx = idx;
        inventorySlotsBG[idx].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.8f);

        if (idx >= objects.Length || objects[idx] == null)
        {
            equippedObject = null;
            equippedObj = null;
            pickupObject = null;
            return;
        }

        equippedObject = objects[idx];
        equippedObject.obj.SetActive(true);
        equippedObject.obj.transform.position = toolRefPoint.transform.position;

        NoCollision();
    }

    void NoCollision() //need to take into account pickUpObject beingHeld == true
    {
        if (pickupObject != null)
        {
            Debug.Log("called");
            pickupObject.beingHeld = true;
        }

        equippedObject.obj.layer = LayerMask.NameToLayer("NoCollision");

        if (equippedObject.obj.transform.childCount == 0) return;
        foreach (Transform child in equippedObject.obj.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("NoCollision");
        }
    }

    void Unequip()
    {
        inventorySlotsBG[equippedIdx].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);

        if (objects[equippedIdx] != null && equippedObject != null)
        {
            objects[equippedIdx].obj.SetActive(false);
            equippedObject = null;
        }
        equippedIdx = -1;
    }

    public void DropEquippedObject()
    {
        if (equippedObj == null)
        {
            Debug.Log("can't drop");
            return;
        }

        if (pickupObject == null)
        {
            Debug.Log("no pickupObject script");
            return;
        }

        pickupObject.beingHeld = false;
        inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        inventorySlotsBG[equippedIdx].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
        objects[equippedIdx] = null;
        equippedObject = null;

        equippedObj.layer = pickupObject.objLayer;
        equippedObj.GetComponent<PickupObject>().inInventory = false;
        equippedObj = null;
    }

    public void RemoveEquippedObject()
    {
        inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        inventorySlotsBG[equippedIdx].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
        objects[equippedIdx] = null;
        equippedObject = null;

        equippedIdx = -1;
        Destroy(equippedObj);
        equippedObj = null;
    }

    public void RemoveMultiObjects(List<InventoryObject> multiObjects)
    {
        List<int> indicesToRemove = new List<int>();

        //Collect matching indices
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null) continue;

            foreach (InventoryObject n in multiObjects)
            {
                if (objects[i] == n)
                {
                    indicesToRemove.Add(i);
                    break; // Prevent adding same index multiple times
                }
            }
        }

        //Remove from highest to lowest index
        indicesToRemove.Sort((a, b) => b.CompareTo(a));

        foreach (int i in indicesToRemove)
        {
            if (i == equippedIdx)
            {
                RemoveEquippedObject();
            }
            else
            {
                inventorySlots[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                inventorySlotsBG[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
                objects[i] = null;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i <= 4; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Equip(i);
            }
        }

        /*if (equippedIdx > -1 && Input.GetKey(KeyCode.Q))
        {
            Unequip();
        }*/

        if (equippedIdx > -1 && equippedIdx < objects.Length && objects[equippedIdx] != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropEquippedObject();
        }

        // handle holding of tool/object
        if (equippedIdx > -1 && equippedObject != null && equippedObject.obj != null)
        {
            equippedObj = equippedObject.obj;
            pickupObject = equippedObj.GetComponent<PickupObject>();
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
