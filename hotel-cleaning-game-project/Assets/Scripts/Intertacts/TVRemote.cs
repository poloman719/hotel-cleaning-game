using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TVRemote : MonoBehaviour, IInteractable
{
    public string interactTextOff = "Turn Off TV (F)";
    public string notOnText = "[The TV is already off.]";

    private GameObject failedText;
    private TextMeshProUGUI interactTextUI;
    private GameObject tvVid;
    private RaycastManager raycastManager;

    void Start()
    {
        raycastManager = FindObjectOfType<RaycastManager>();
        failedText = GameObject.Find("FailedText");
        interactTextUI = GameObject.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        tvVid = GameObject.Find("TVVideo");
    }

    void Update()
    {
        if (tvVid != null && tvVid.activeInHierarchy && raycastManager.detecting && raycastManager.objectDetected == gameObject)
            interactTextUI.color = new Color(1f, 1f, 1f, 1f);

        if (!raycastManager.detecting) return;
        if (raycastManager.objectDetected != gameObject) return;
        if (tvVid == null) return;
        if (tvVid.activeInHierarchy) return;

        interactTextUI.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void Interact()
    {
        if (tvVid == null || (tvVid != null && !tvVid.activeInHierarchy))
        {
            failedText.GetComponent<TextMeshProUGUI>().text = notOnText;
            failedText.GetComponent<Animator>().SetBool("flash", true);
        }

        if (tvVid != null)
        {
            tvVid.SetActive(false);
        }
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
            displayedText.text = interactTextOff;
    }
}
