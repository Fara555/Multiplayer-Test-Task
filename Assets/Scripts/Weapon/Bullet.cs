using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject enviromentHitPrefab;
    [SerializeField] private GameObject playerHitPrefab;

    [Header("Time")] 
    [SerializeField] private float  timeToDestroyBullet;

    private AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.Find("Sounds").GetComponent<AudioController>();
    }

    private void Start()
    {
        StartCoroutine(waitForDestroy()); //Start coroutine to destroy bullet whet its spawn
    }

    private void OnTriggerEnter2D(Collider2D collider) //check if it was player, then call DecreaseHeath RPC function to decrease health of player and destroy bullet
                                                       //(Coroutine because of some issue with destroy bullet)
    {
        if (collider.gameObject.name == "Player(Clone)")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            Health playerHealth = collider.GetComponent<Health>();
            if (player.playerView.IsMine)
            {
                player.playerView.RPC("RPC_DecreaseHealth", RpcTarget.AllBuffered, playerHealth.currentHealth);
            }
            GameObject playerHit = PhotonNetwork.Instantiate(playerHitPrefab.name, gameObject.transform.position, gameObject.transform.rotation); //initiate hit animaion when bullet hit player 
            audioController.playerHitSound.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Coin")
        {

        }
        else
        {            
            GameObject enviromentHit = PhotonNetwork.Instantiate(enviromentHitPrefab.name, gameObject.transform.position, gameObject.transform.rotation); //initiate hit animaion when bullet hit any enviroment 
            audioController.enviromentHitSound.Play();
            Destroy(gameObject);
        }
    }
    IEnumerator waitForDestroy() //Coroutine for destroy objects
    {
        yield return new WaitForSeconds(timeToDestroyBullet);
        Destroy(gameObject);
    }
}
