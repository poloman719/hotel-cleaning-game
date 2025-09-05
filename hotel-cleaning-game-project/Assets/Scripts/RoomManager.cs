using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tasks: 
/// modify SpawnNextRoom to calculate if a roomAnomaly should be spawned before defaultRoom
/// optional QoL: create a default list of norm, eerie, and room anomalies that can spawn in ANY day. This can reduce amount of defaults needed to add in inspector for each day
/// finish DestroyPrevRoom
/// finish isRoomCleaned
/// </summary>
public class RoomManager : MonoBehaviour
{
    public static RoomManager rmInstance { get; private set; } //allows calling RoomManager methods without GetComponent

    [HideInInspector] public int currentDay = 1;
    [HideInInspector] public int maxDay = 7;
    [HideInInspector] public int currentRoom = 1;
    [HideInInspector] public int maxRoom = 20;

    [SerializeField] private GameObject defaultRoomPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnPointXAmount;
    [SerializeField] private float spawnPointZAmount;

    public List<AnomalyDayConfig> anomalyDayConfigs;
    private Dictionary<int, AnomalyDayConfig> dayConfigLookup;

    //Day 1: first 4 rooms no eerieAnomals or roomAnomals; have at least 1 room with tool usage
    #region AnomalyDay + AnomalyRoom Configurations

    [System.Serializable]
    public class AnomalyDayConfig //default room anomaly spawning algorithm for a specifc day
    {
        public int day;
        public List<NormAnomalGroup> defaultNormAnomalies; //i.e. dirty towels, crooked paintings, missing soap, etc
        public List<GameObject> defaultEerieAnomalies; //i.e cans of soda exploding, upside down room, etc
        public List<GameObject> defaultRoomAnomalies; //i.e snowy room, tree in the room, ceiling hole, etc
        public int defaultNormCount;
        public bool allowEerie;
        public bool allowRoom;
        public List<RoomAnomalyOverride> roomOverrides;
    }

    [System.Serializable]
    public class RoomAnomalyOverride //rooms that differ from the typical room spawning for a specific day
    {
        public int roomNumber;
        public List<NormAnomalGroup> customNormAnomalies;
        public List<GameObject> customEerieAnomalies;
        public List<GameObject> customRoomAnomalies;
        public int? customNormCount;
        public bool? allowEerieOverride;
        public bool? allowRoomOverride;
    }

    // Helper data class to hold the final resolved config for a specific day & room
    public class RoomAnomalyConfig
    {
        public int day;
        public int roomNumber;
        public List<NormAnomalGroup> normAnomalies;
        public List<GameObject> allowedEerieAnomalies;
        public List<GameObject> allowedRoomAnomalies;
        public int normAnomalCount;
        public bool allowEerie;
        public bool allowRoom;
    }

    #endregion

    void Awake()
    {
        if (rmInstance == null)
            rmInstance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dayConfigLookup = new Dictionary<int, AnomalyDayConfig>();
        foreach (var anomalyDayConfig in anomalyDayConfigs)
        {
            dayConfigLookup[anomalyDayConfig.day] = anomalyDayConfig;
        }
    }

    #region Spawning; Destroying; Checking Rooms

    public void SpawnNextRoom()
    {
        currentRoom++;
        if (currentRoom > maxRoom)
            currentRoom = 1;

        GameObject spawnedRoom = Instantiate(defaultRoomPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnPoint.position += new Vector3(spawnPointXAmount, 0, spawnPointZAmount);

        SpawnAnomaliesForRoom(spawnedRoom);
    }

    public void DestroyPrevRoom()
    {

    }

    private bool isRoomCleaned()
    {
        return true;
    }

    #endregion

    #region Retrieving Room Configs & Applying to Anomaly Spawning

    // Get the config for current day & room, applying overrides
    public RoomAnomalyConfig GetRoomAnomalyConfig(int day, int room)
    {
        if (!dayConfigLookup.ContainsKey(day))
        {
            return null;
        }

        AnomalyDayConfig dayConfig = dayConfigLookup[day];
        RoomAnomalyOverride roomOverride = dayConfig.roomOverrides?.Find(r => r.roomNumber == room); //finds first override in dayConfig.roomOverrides where roomNumber == room; returns null otherwise

        return new RoomAnomalyConfig
        {
            day = day,
            roomNumber = room,
            normAnomalies = roomOverride?.customNormAnomalies ?? dayConfig.defaultNormAnomalies, //if roomOverride == null, use dayConfig.defaultNormAnomalies
            allowedEerieAnomalies = roomOverride?.customEerieAnomalies ?? dayConfig.defaultEerieAnomalies,
            allowedRoomAnomalies = roomOverride?.customRoomAnomalies ?? dayConfig.defaultRoomAnomalies,
            normAnomalCount = roomOverride?.customNormCount ?? dayConfig.defaultNormCount,
            allowEerie = roomOverride?.allowEerieOverride ?? dayConfig.allowEerie,
            allowRoom = roomOverride?.allowRoomOverride ?? dayConfig.allowRoom
        };
    }

    public void SpawnAnomaliesForRoom(GameObject spawnedRoom)
    {
        RoomAnomalyConfig config = GetRoomAnomalyConfig(currentDay, currentRoom);
        if (config == null)
        {
            Debug.LogWarning("No anomaly config found for day " + currentDay + " room " + currentRoom);
            return;
        }

        // Spawn Normal Anomalies
        List<NormAnomalGroup> normGroups = GetRandomNormAnomalyGroups(config.normAnomalies, config.normAnomalCount);
        foreach (var normGroup in normGroups)
        {
            if (normGroup.items.Count == 0) continue; // continue = move on to next iteration of foreach

            GameObject item = normGroup.items[Random.Range(0, normGroup.items.Count)]; //this only allows spawning 1 random item of a normalAnomaly i.e. dirty towels. Adjust or add-on as desired 
            Instantiate(item, spawnedRoom.transform);
        }

        // Spawn one eerie anomaly if allowed
        if (config.allowEerie && config.allowedEerieAnomalies.Count > 0)
        {
            GameObject eerie = config.allowedEerieAnomalies[Random.Range(0, config.allowedEerieAnomalies.Count)];
            Instantiate(eerie, spawnedRoom.transform);
        }

        // Spawn one room anomaly if allowed
        else if (config.allowRoom && config.allowedRoomAnomalies.Count > 0)
        {
            GameObject roomAnomaly = config.allowedRoomAnomalies[Random.Range(0, config.allowedRoomAnomalies.Count)];
            Instantiate(roomAnomaly, spawnedRoom.transform);
        }
    }

    #endregion

    #region Normal Anomaly Group

    //Allows nested lists in Unity
    [System.Serializable]
    public class NormAnomalGroup
    {
        public string normAnomalName;
        public float chance;
        public List<GameObject> items;
    }

    //Calculates totalChance by adding all chances.
    //Generates random number between 0 - totalChance.
    //Adds each normAnomalGroup's chances one-by-one, and once that (aka runningNum) surpasses the random number, add the last normAnomalGroup whos chance was added.
    //Repeats process int amount of times
    public List<NormAnomalGroup> GetRandomNormAnomalyGroups(List<NormAnomalGroup> source, int amount)
    {
        List<NormAnomalGroup> groups = new List<NormAnomalGroup>();

        float totalChance = 0f;
        foreach (NormAnomalGroup normAnomalGroup in source)
        {
            totalChance += normAnomalGroup.chance;
        }

        for (int i = 0; i < amount; i++)
        {
            float randomNum = Random.value * totalChance;
            float runningNum = 0f;
            foreach (NormAnomalGroup normAnomalGroup in source)
            {
                runningNum += normAnomalGroup.chance;
                if (runningNum >= randomNum)
                {
                    groups.Add(normAnomalGroup);
                    break;
                }
            }
        }

        return groups;
    }

    #endregion
}
