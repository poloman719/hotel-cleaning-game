using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public string interactText = "Flip Light Switch (F)";

    private GameObject ceilingLightPoint;
    private GameObject ceilingLightDir;
    private Animator ceilingLightAnim;
    private Transform parentTransform;

    bool switchState = false;

    void Start()
    {
        parentTransform = transform.parent;
        ceilingLightPoint = parentTransform.Find("CeilingFan").GetChild(0).gameObject;
        ceilingLightDir = parentTransform.Find("CeilingFan").GetChild(1).gameObject;
        ceilingLightAnim = parentTransform.Find("CeilingFan").GetChild(2).GetComponent<Animator>();
    }

    public void Interact()
    {
        switchState = !switchState;

        if (switchState)
            TurnOn();
        else
            TurnOff();
    }

    public void DisplayInteractText(TextMeshProUGUI displayedText)
    {
        displayedText.text = interactText;
    }

    void TurnOn()
    {
        ceilingLightPoint.SetActive(true);
        ceilingLightDir.SetActive(true);

        ceilingLightAnim.SetFloat("FanSpeed", 1f);
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);
    }

    void TurnOff()
    {
        ceilingLightPoint.SetActive(false);
        ceilingLightDir.SetActive(false);

        ceilingLightAnim.SetFloat("FanSpeed", 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}

