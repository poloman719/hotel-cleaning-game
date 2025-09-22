using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorHandle : MonoBehaviour //Same as TowelBin script logic
{
    [SerializeField] private List<InventoryObject> doorTagList; //Should only carry 1 door tag item. list is neccesary because playerInventory script only has removeMultiObj function

    public string interactText = "Place Tag (F)";
    public string cantInteractText = "[You don't have any door tags.]";

    public GameObject roomDoorTag;

    private TextMeshProUGUI pickUpText;
    private GameObject failedText;
    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;
    [SerializeField] private bool hasDoorTag = false;

    void Start()
    {
        raycastManager = FindObjectOfType<RaycastManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        pickUpText = GameObject.Find("PickupText").GetComponent<TextMeshProUGUI>();
        failedText = GameObject.Find("FailedText");
    }

    void Update()
    {
        if (!raycastManager.detecting) return;
        if (raycastManager.objectDetected != gameObject) return;

        pickUpText.text = interactText;
        if (!hasDoorTag) pickUpText.color = new Color(1f, 1f, 1f, 0.5f);

        foreach (InventoryObject n in playerInventory.objects)
        {
            if (n != null && n.obj.name.Contains("door tag") && doorTagList.Count < 1) //checks if doorTagList doesn't store a doorTag
            {
                doorTagList.Add(n);
                pickUpText.color = new Color(1f, 1f, 1f, 1f);
                break;
            }
        }

        if (doorTagList.Count > 0) hasDoorTag = true;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hasDoorTag)
            {
                failedText.GetComponent<TextMeshProUGUI>().text = interactText;
                failedText.GetComponent<Animator>().SetBool("flash", true);
            }
            else if (hasDoorTag)
            {
                roomDoorTag.SetActive(true);
                playerInventory.RemoveMultiObjects(doorTagList);
                doorTagList.Clear();
                pickUpText.color = new Color(1f, 1f, 1f, 0.5f);
                hasDoorTag = false;
            }
        }
    }
}