using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowelBin : MonoBehaviour
{
    public string towelText = "Drop dirt towel (E)";

    //[You're not holding the proper equipment.] if the player tries to clean something that requires a tool
    public string noTowelText = "[You don't have any dirty towels.]"; 
    public Transform spawnTransform;

    private PlayerInventory playerInventory;
    private RaycastManager raycastManager;
    private TextMeshProUGUI interactText;
    private GameObject failedText;

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
        interactText.color = new Color(1f, 1f, 1f, 0.5f);

        if (playerInventory.equippedObj != null && playerInventory.equippedObj.name.Contains("Dirty Towel"))
            interactText.color = new Color(1f, 1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory.equippedObj == null || !playerInventory.equippedObj.name.Contains("Dirty Towel"))
            {
                failedText.GetComponent<TextMeshProUGUI>().text = noTowelText;
                failedText.GetComponent<Animator>().SetBool("flash", true);
            }
            else if (playerInventory.equippedObj.name == "Dirty Towel")
            {
                GameObject dirtyTowel = playerInventory.equippedObj;
                dirtyTowel.layer = 0;
                Instantiate(dirtyTowel, spawnTransform.position, Quaternion.identity);
                playerInventory.RemoveObject();
                interactText.color = new Color(1f, 1f, 1f, 1f);
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
