using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CollectableCoin : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer coinSprite;
    [SerializeField] private SpriteRenderer shadowSprite;
    private CircleCollider2D _collider;
    private AudioController audioController;


    private void Awake()//initiate components
    {
        audioController = GameObject.Find("Sounds").GetComponent<AudioController>();
        particle = gameObject.GetComponent<ParticleSystem>();
        coinSprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //OnTriggerEnter check if it was Player, then if it was current player client send RPC function - "IncreaseCoins"
                                                        //to other players and call coroutine to destroy coin
    {
        if (collision.name == "Player(Clone)")
        {
            particle.Play();
            _collider.enabled = false;
            coinSprite.enabled = false;
            shadowSprite.enabled = false;
            audioController.coinPickUpSound.Play();
            StartCoroutine(waitForDestroy());
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController.playerView.IsMine)
            {
                playerController.playerView.RPC("IncreaseCoins", RpcTarget.AllBuffered, playerController.coinCurrentAmount);
            }
        }
    }

    IEnumerator waitForDestroy() //Coroutine for destroy coin
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
