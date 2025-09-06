using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowelBin : MonoBehaviour
{
    [SerializeField] private List<InventoryObject> dirtyTowels;

    public string towelText = "Drop dirt towel (E)";
    //[You're not holding the proper equipment.] if the player tries to clean something that requires a tool
    public string noTowelText = "[You don't have any dirty towels.]"; 
    public Transform spawnTransform;

    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;
    private TextMeshProUGUI interactText;
    private GameObject failedText;
    [SerializeField] private bool hasDirtyTowel = false;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        raycastManager = FindObjectOfType<RaycastManager>();
        interactText = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        failedText = GameObject.Find("FailedText");
    }

    void Update()
    {
        if (!raycastManager.detecting) return;
        if (raycastManager.objectDetected != gameObject && !IsChildOfThis(raycastManager.objectDetected)) return;

        interactText.text = towelText;
        if (!hasDirtyTowel) interactText.color = new Color(1f, 1f, 1f, 0.5f);

        foreach (InventoryObject n in playerInventory.objects)
        {
            if (n != null && n.obj.name.Contains("Dirty Towel") && hasDirtyTowel == false) //hasDirtyTowel check here to avoid running again once hasDirtyTowel == true
            {
                dirtyTowels.Add(n);
                interactText.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        if (dirtyTowels.Count > 0) hasDirtyTowel = true;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hasDirtyTowel) //playerInventory.equippedObj == null || !playerInventory.equippedObj.name.Contains("Dirty Towel"))
            {
                failedText.GetComponent<TextMeshProUGUI>().text = noTowelText;
                failedText.GetComponent<Animator>().SetBool("flash", true);
            }
            else if (hasDirtyTowel) //playerInventory.equippedObj.name == "Dirty Towel")
            {
                /* GameObject dirtyTowel = playerInventory.equippedObj;
                dirtyTowel.layer = 0;
                Instantiate(dirtyTowel, spawnTransform.position, Quaternion.identity);
                playerInventory.RemoveEquippedObject();
                */
                playerInventory.RemoveMultiObjects(dirtyTowels);
                dirtyTowels.Clear();
                interactText.color = new Color(1f, 1f, 1f, 1f);
                hasDirtyTowel = false;
            }
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
