using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingScript : MonoBehaviour
{
    public string furnitureTag = "Furniture";
    private GameObject pushedObject;
    private bool pushing = false;
    private Vector3 prevPosition;
    private RaycastManager rm;

    void Start()
    {
        rm = GetComponent<RaycastManager>();
        prevPosition = transform.position;
    }

    void Update()
    {
        if (!pushing && Input.GetMouseButtonDown(0) && rm.objectDetected && rm.objectDetected.CompareTag(furnitureTag))
        {
            Debug.Log("Push Attempted");
            pushedObject = rm.objectDetected;
            pushing = true;
        }
        // handling pushing
        if (pushing)
        {
            // Move Item to a certain distance from the camera on the raycast
            // pushedObject.transform.position = Vector3.MoveTowards(pushedObject.transform.position, refPoint.transform.position, speed * Time.deltaTime);
            Vector3 positionChange = transform.position - prevPosition;

            if (rm.objectDetected.transform.parent.CompareTag(furnitureTag))
            {
                pushedObject = rm.objectDetected.transform.parent.gameObject;
            }

            pushedObject.transform.position += positionChange;

            if (!Input.GetMouseButton(0))
            {
                pushing = false;
            }
        }
        prevPosition = transform.position;
    }
}
