using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDistMovementScript : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float distMin = 1;
    public float distMax = 5;
    public float currentDist = 2.5f;

    void Start()
    {

    }

    void Update()
    {
        float moveDist = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        if (currentDist + moveDist > distMin && currentDist + moveDist < distMax)
        {
            currentDist += moveDist;
            transform.Translate(Vector3.forward * moveDist);
        }
    }
}

