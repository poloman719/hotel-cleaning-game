using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowelBin : MonoBehaviour
{
    public string towelText = "Drop dirt towel (E)";
    public string noTowelText = "[You don't have any dirty towels.]";

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

        if (playerInventory.equippedObj != null && playerInventory.equippedObj.name == "Dirty Towel")
            interactText.color = new Color(1f, 1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory.equippedObj == null || playerInventory.equippedObj.name != "Dirty Towel")
            {
                failedText.GetComponent<TextMeshProUGUI>().text = noTowelText;
                failedText.GetComponent<Animator>().SetBool("flash", true);
            }
            //playerInventory.AddObject();
            //gameObject.SetActive(false);
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
