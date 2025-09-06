using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingScript : MonoBehaviour
{
    public string itemTag = "Item";
    private GameObject heldObject;
    private bool holding = false;
    public GameObject refPoint;
    public float pullForce;
    public float tolerance;
    private RaycastManager rm;
    public GameObject player;
    public float rotationSpeed;
    public GameObject room;

    void Start() 
    {
        rm = GetComponent<RaycastManager>();
        room = GameObject.Find("Objects/Furniture");
    }

    void Update()
    {
        if (!holding && Input.GetMouseButtonDown(0) && rm.objectDetected && rm.objectDetected.CompareTag(itemTag))
        {
            Debug.Log("Grab Attempted");
            heldObject = rm.objectDetected;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            heldObject.GetComponent<Rigidbody>().freezeRotation = true;
            heldObject.GetComponent<PickupObject>().BeingHeld(true);
            holding = true;
        }
        if (holding)
        {
            // Check for object dist
            if (Vector3.Distance(heldObject.transform.position, refPoint.transform.position) > tolerance)
            {
                heldObject.GetComponent<Rigidbody>().velocity = (refPoint.transform.position - heldObject.transform.position) * Vector3.Distance(heldObject.transform.position, refPoint.transform.position) * pullForce;
            }
            else
            {
                heldObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }

            // Rotations
            if (Input.GetKey(KeyCode.R))
            {
                heldObject.GetComponent<Rigidbody>().freezeRotation = false;
                player.GetComponent<PlayerMovement>().rotationLocked = true;
                Debug.Log("Attempted Rotate");

                float rotateX = Input.GetAxis("Mouse Y");
                float rotateY = Input.GetAxis("Mouse X");

                heldObject.transform.SetParent(refPoint.transform);
                Vector3 prevRot = refPoint.transform.rotation.eulerAngles;

                refPoint.transform.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime * rotationSpeed, Space.Self);
                refPoint.transform.Rotate(new Vector3(rotateX, 0, 0) * Time.deltaTime * rotationSpeed, Space.Self);


                heldObject.transform.SetParent(room.transform);

                refPoint.transform.rotation = Quaternion.Euler(prevRot);

                heldObject.GetComponent<Rigidbody>().freezeRotation = true;

                /*
                heldObject.transform.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime * rotationSpeed, Space.Self);
                heldObject.transform.Rotate(new Vector3(0, 0, rotateX) * Time.deltaTime * rotationSpeed, Space.Self);
                */
            }
            else
            {
                heldObject.transform.SetParent(room.transform);
                heldObject.GetComponent<Rigidbody>().freezeRotation = true;
                player.GetComponent<PlayerMovement>().rotationLocked = false;
            }

            // Check for left click hold
            if (!Input.GetMouseButton(0))
            {
                heldObject.GetComponent<Rigidbody>().useGravity = true; // GetComponent<Rigidbody>().freezeRotation = true;
                heldObject.GetComponent<Rigidbody>().freezeRotation = false;
                heldObject.transform.SetParent(room.transform);
                player.GetComponent<PlayerMovement>().rotationLocked = false;
                holding = false;
                heldObject.GetComponent<PickupObject>().BeingHeld(false);
            }
        }
    }
}
