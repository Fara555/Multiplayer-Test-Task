using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [Header("PlayerPrefab : ")]
    [SerializeField] private GameObject playerPrefab;

    [Header("SpawnValues")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private GameObject[] obstacles;



    private void Start()// initiate player in network at random position 
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (randomPos != obstacles[i].transform.position)
            {
                player.transform.position = randomPos;
            }
        }
    }
}
