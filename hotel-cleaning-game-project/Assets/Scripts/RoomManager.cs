using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager rmInstance { get; private set; } //allows calling RoomManager methods without GetComponent

    public int currentDay = 1;
    public int maxDay = 7;

    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnPointXAmount;
    [SerializeField] private float spawnPointZAmount;

    private List<Action<GameObject>> anomalyAlgorithms;
    [SerializeField] private List<GameObject> normalAnomalies; //i.e. dirty towels, crooked paintings, etc
    [SerializeField] private List<GameObject> anomalies; //i.e cans of soda exploding, upside down room, etc

    void Awake()
    {
        if (rmInstance == null)
            rmInstance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //Each day has its own SpawnAnomalies Method for different randomization algorithms
        anomalyAlgorithms = new List<Action<GameObject>>()
        {
            SpawnAlg1,
            SpawnAlg2,
            SpawnAlg3,
            SpawnAlg4,
            SpawnAlg5,
            SpawnAlg6,
            SpawnAlg7
        };
    }

    public void SpawnNextRoom()
    {
        GameObject spawnedRoom = Instantiate(roomPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnPoint.position += new Vector3(spawnPointXAmount, 0, spawnPointZAmount);

        int index = Mathf.Clamp(currentDay - 1, 0, anomalyAlgorithms.Count - 1);
        anomalyAlgorithms[index]?.Invoke(spawnedRoom);
    }

    #region Anomaly Algorithms depending on day

    public void SpawnAlg1(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg2(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg3(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg4(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg5(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg6(GameObject spawnedRoom)
    {

    }

    public void SpawnAlg7(GameObject spawnedRoom)
    {

    }

    #endregion

    private void SpawnAnomalies(GameObject room, List<GameObject> normAnomalsChosen, List<GameObject> anomalsChosen, int normAnomalsCount, int anomalsCount)
    {

    }

    public void DestroyPrevRoom()
    {
        
    }

    private bool isRoomCleaned()
    {
        return true;
    }
}
