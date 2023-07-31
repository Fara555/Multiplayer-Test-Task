using UnityEngine;
using TMPro;
using Photon.Pun;

public class CoinCounter : MonoBehaviour
{
    [Header("Main Ref: ")]
    public TMP_Text counterText; //Coin display

    [HideInInspector] public int currentCoins = 0; //Current amount of coins

    [PunRPC]
    void IncreaseCoins(string coinsText) //RPC function for increase coins in individual players
    {
        this.currentCoins += +1;
        coinsText = currentCoins.ToString();
        counterText.text = coinsText;

    }
}
