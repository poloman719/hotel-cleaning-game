using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject ceilingLight;
    [SerializeField] private Animator ceilingLightAnim;
    [SerializeField] private Transform parentTransform;

    bool switchState = false;

    void Start()
    {
        parentTransform = transform.parent;
        ceilingLight = parentTransform.Find("CeilingFan").GetChild(0).gameObject;
        ceilingLightAnim = parentTransform.Find("CeilingFan").GetChild(1).GetComponent<Animator>();
    }

    public void Interact()
    {
        switchState = !switchState;

        if (switchState)
            TurnOn();
        else
            TurnOff();
    }

    void TurnOn()
    {
        ceilingLight.SetActive(true);
        ceilingLightAnim.SetFloat("FanSpeed", 1f);
        transform.rotation = Quaternion.Euler(180f, 0f, 0f);
    }

    void TurnOff()
    {
        ceilingLight.SetActive(false);
        ceilingLightAnim.SetFloat("FanSpeed", 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}

