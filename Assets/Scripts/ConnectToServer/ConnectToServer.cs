using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //connect client to the server
    }

    public override void OnConnectedToMaster()//on connected client to master server
    {
        PhotonNetwork.JoinLobby(); //join lobby 
    }

    public override void OnJoinedLobby()//on joined lobby 
    {
        PhotonNetwork.LoadLevel("Lobby");//load lobby scene
    }
}

