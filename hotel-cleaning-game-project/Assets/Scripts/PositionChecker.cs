using System;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    public GameObject itself;
    public GameObject itemChecked;
    public Vector3 reqRotation;
    public bool itemInPlace;
    public float distTolerance;
    public float rotTolerance;
    public bool rotClear = false;
    public bool[] checkAxis = { true, true, true };
    void Start()
    {
        if (!itemChecked)
        {
            Debug.Log("No Object Assigned to Checker");
            Destroy(itself);
        }
    }

    void Update()
    {
        // Check Rotation

        float[] rotations = { Math.Abs(reqRotation.x - itemChecked.transform.rotation.eulerAngles.x), Math.Abs(reqRotation.y - itemChecked.transform.rotation.eulerAngles.y), Math.Abs(reqRotation.z - itemChecked.transform.rotation.eulerAngles.z) };
        for (int i = 0; i < 3; i++)
        {
            if (!checkAxis[i])
            {
                rotations[i] = 0;
            }
        }
        foreach (float i in rotations)
        {
            if (i < 360 - rotTolerance && i > rotTolerance)
            {
                rotClear = false;
                break;
            }
            else
            {
                rotClear = true;
            }
        }

        // Check
        if (Vector3.Distance(transform.position, itemChecked.transform.position) < distTolerance && rotClear)
        {
            if (!itemInPlace)
            {
                Debug.Log("done i guess");
                itemInPlace = true;
            }
        }
        else
        {
            itemInPlace = false;
        }
    }
}
