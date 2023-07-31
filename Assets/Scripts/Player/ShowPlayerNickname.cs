using TMPro;
using Photon.Pun;
using UnityEngine;

public class ShowPlayerNickname : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerNameText;

    void Start()
    {
        playerNameText.text = photonView.Owner.NickName;
    }
}
