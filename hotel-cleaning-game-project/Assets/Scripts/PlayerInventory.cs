using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlots;
    // see InventoryTool class implementation in InventoryTool.cs
    [SerializeField] private List<InventoryTool> tools;
    public InventoryTool equippedTool = null;
    private int equippedIdx = -1;
    public GameObject toolRefPoint;
    public float tolerance = 0.1f;
    public float pullForce = 10f;

    /// <summary>
    /// this function will have an "image parameter"; If a grab object is stored, the function will grab the object's icon as a parameter and insert it to the inventory slot
    /// </summary>
    public void AddObject(InventoryTool tool)
    {
        // for (int i = 0; i < inventorySlots.Count; i++)
        // {
        //     if (inventorySlots[i].GetComponent<Image>().sprite == null)
        //     {
        //         inventorySlots[i].GetComponent<Image>().sprite = objectIcon;
        //         inventorySlots[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //         return;
        //     }
        // }
        Debug.Log("adding " + tool.toolType);
        if (tools.Count == inventorySlots.Count) return;
        Image slotImage = inventorySlots[tools.Count].GetComponent<Image>();
        slotImage.sprite = tool.icon;
        slotImage.color = new Color(1f, 1f, 1f, 1f);
        tools.Add(tool);
    }

    void EquipTool(int idx)
    {
        if (idx >= tools.Count) return;
        Debug.Log("equipping tool " + idx);
        if (equippedIdx > -1)
        {
            inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            tools[equippedIdx].toolObject.SetActive(false);
        }
        inventorySlots[idx].GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
        equippedTool = tools[idx];
        equippedIdx = idx;
        equippedTool.toolObject.SetActive(true);
        equippedTool.toolObject.transform.position = toolRefPoint.transform.position;
        equippedTool.toolObject.layer = LayerMask.NameToLayer("NoCollision");
    }

    void UnequipTool()
    {
        inventorySlots[equippedIdx].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        tools[equippedIdx].toolObject.SetActive(false);
        equippedTool = null;
        equippedIdx = -1;
    }

    public void RemoveObject()
    {

    }

    void Update()
    {
        // check for user input
        if (equippedIdx != 0 && Input.GetKey(KeyCode.Alpha1))
        {
            EquipTool(0);
        }
        if (equippedIdx != 1 && Input.GetKey(KeyCode.Alpha2))
        {
            EquipTool(1);
        }
        if (equippedIdx != 2 && Input.GetKey(KeyCode.Alpha3))
        {
            EquipTool(2);
        }
        if (equippedIdx != 3 && Input.GetKey(KeyCode.Alpha4))
        {
            EquipTool(3);
        }
        if (equippedIdx != 4 && Input.GetKey(KeyCode.Alpha5))
        {
            EquipTool(4);
        }
        if (equippedIdx > -1 && Input.GetKey(KeyCode.Q))
        {
            UnequipTool();
        }

        // handle holding of tool
        if (equippedIdx > -1)
        {
            GameObject equippedObj = equippedTool.toolObject;
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
}
