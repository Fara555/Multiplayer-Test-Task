using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInput; //room name create input
    [SerializeField] private InputField joinInput;// room name join input
    [SerializeField] private InputField playerNickNameInput; //nickname input
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text); // create room with create input name
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text); // join room by join input name
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game"); // load game scene
    }

    public void SetPlayersNickName()//set player nick name by  nickname input
    {
        PhotonNetwork.NickName = playerNickNameInput.text; 
    }
}
