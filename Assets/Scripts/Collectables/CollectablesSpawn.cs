using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollectablesSpawn : MonoBehaviour
{
    [Header("Main Ref: ")]
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject[] obstacles;
    [Range(3,15)]
    [SerializeField] private float spawnRate;

    private void Start()
    {
        StartCoroutine(waitForSpawn()); //start endless coroutine
    }

    private void RPC_SpawnCollectables() //Initiate prefab of coins on place where doesnt have obstacles with RPC Function
    {
        Vector3 pos = new Vector3(Random.Range(-8, 14), Random.Range(-6, 8), 0);
        GameObject a = PhotonNetwork.Instantiate(coin.name, pos, Quaternion.identity);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (pos != obstacles[i].transform.position)
            {
                a.transform.position = pos;
            }
        }
    }
    private IEnumerator waitForSpawn()//Endless coroutine for spawn coins
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-8, 14), Random.Range(-6, 8), 0);
            RPC_SpawnCollectables();
        }
    }
}
