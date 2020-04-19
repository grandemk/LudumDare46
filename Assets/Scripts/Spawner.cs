using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Shop shop;
    public GameObject kidPrefab = null;
    public GameObject spawnContainer = null;
    private IEnumerator spawnCoroutine;
    void Start()
    {
        spawnCoroutine = SpawnRoutine();
        StartCoroutine(spawnCoroutine);
    }

    int ChooseSpawnNumber()
    {
        return Random.Range(1, 3);
    }

    void SpawnKid()
    {
        GameObject obj = Instantiate(kidPrefab, spawnContainer.transform);
        int side = Random.Range(0, 4);
        float t = Random.Range(0.0f, 1.0f);
        Vector3 worldPos = Vector3.zero;

        if (side == 0)
        {
            worldPos.x = shop.tilemap.cellBounds.xMin;
            worldPos.y = Mathf.Lerp(shop.tilemap.cellBounds.yMin, shop.tilemap.cellBounds.yMax, t);
        }
        else if (side == 1)
        {
            worldPos.x = shop.tilemap.cellBounds.xMax;
            worldPos.y = Mathf.Lerp(shop.tilemap.cellBounds.yMin, shop.tilemap.cellBounds.yMax, t);
        }
        else if (side == 2)
        {
            worldPos.x = Mathf.Lerp(shop.tilemap.cellBounds.xMin, shop.tilemap.cellBounds.xMax, t);
            worldPos.y = shop.tilemap.cellBounds.yMin;
        }
        else if (side == 3)
        {
            worldPos.x = Mathf.Lerp(shop.tilemap.cellBounds.xMin, shop.tilemap.cellBounds.xMax, t);
            worldPos.y = shop.tilemap.cellBounds.yMax;
        }
        obj.transform.position = worldPos;
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            var numFlowers = shop.GetNumFlowers();
            var maxSimultaneousSpawn = 10 + (numFlowers * numFlowers) / 50;
            var numSimultaneousSpawn = spawnContainer.transform.childCount;
            if(numSimultaneousSpawn < maxSimultaneousSpawn)
            {
                int spawnNum = ChooseSpawnNumber();
                for(int i = 0; i < spawnNum; ++i)
                    SpawnKid();
                Debug.Log("spawned " + spawnNum.ToString() + " Kid");
            }


            var spawnIntervalDelay = 1f;
            if (numFlowers > 70)
                spawnIntervalDelay = 0.5f;
            yield return new WaitForSeconds(spawnIntervalDelay);
        }
    }
}
