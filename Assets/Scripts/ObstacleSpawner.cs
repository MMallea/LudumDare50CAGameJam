using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnInterval = 5;
    public float dontSpawnHeight;
    public float startFrequency;
    public float endFrequency;
    public Vector3 spawnZoneSize;
    public Transform groundTransform;
    public List<GameObject> bounceObjectPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        float startHeight = groundTransform.position.y + spawnZoneSize.y;
        float endHeight = groundTransform.position.y + dontSpawnHeight;
        float spawnHeight = groundTransform.position.y + spawnZoneSize.y;
        float frequency = startFrequency;

        while (spawnHeight > endHeight)
        {
            for(int i = 0; i < Mathf.Round(frequency); i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(groundTransform.position.x - spawnZoneSize.x, groundTransform.position.x + spawnZoneSize.x), 
                    spawnHeight, Random.Range(groundTransform.position.z - spawnZoneSize.z, groundTransform.position.z + spawnZoneSize.z));
                GameObject prefab = bounceObjectPrefabs[Random.Range(0, bounceObjectPrefabs.Count)];

                Instantiate(prefab, spawnPos, Quaternion.identity, null);
            }

            spawnHeight -= spawnInterval;
            //Assuming end frequency is greater than start frequency, get percentage
            float heightPercentage = 1 -(Mathf.Abs(spawnHeight - endHeight) / Mathf.Abs(startHeight - endHeight));
            frequency = startFrequency + (((endFrequency - startFrequency) * heightPercentage));
        }
    }
}
