using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int currentHealth;
    [HideInInspector] public GameController gameController;
    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] heartsIcons;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    private PhotonView pv;
    private CoinCounter coins;


    private void Start()
    {
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        pv = GetComponent<PhotonView>();
        coins = GetComponent<CoinCounter>();
    }
    private void Update()
    {
        if (currentHealth > maxHealth) // set right amount of player health
        {
            currentHealth = maxHealth;
        }

        for (int i = 0; i < heartsIcons.Length; i++) //change heart icons depending on current player health
        {
            if (i < currentHealth)
            {
                heartsIcons[i].sprite = fullHeartSprite;
            }
            else
            {
                heartsIcons[i].sprite = emptyHeartSprite;
            }

            if (i < maxHealth)
            {
                heartsIcons[i].enabled = true;
            }
            else
            {
                heartsIcons[i].enabled = false;
            }
        }

        if (currentHealth == 0) //check if players dead, then activate deathPanel(UI) and set winner and looser nicknames, amout of coins they collected
        {
            gameController.deathPanel.SetActive(true);
            gameController.looserName.text = pv.Owner.NickName;
            gameController.looserCoinCount.text = coins.currentCoins.ToString();
        }
        else
        {
            gameController.winnerName.text = pv.Owner.NickName;
            gameController.winnerCoinCount.text = coins.currentCoins.ToString();
        }
    }

    [PunRPC]
    private void RPC_DecreaseHealth(int health) //RPC fucntion for decrease healt
    {
        health--;
        this.currentHealth = health;
    }
}
