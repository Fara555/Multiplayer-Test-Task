using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [Header("ScriptRef: ")]
    [SerializeField] private CollectablesSpawn collectablesSpawn;

    [Header("UI Elements:")]
    public GameObject waitingPanel;
    public GameObject deathPanel;
    public TMP_Text winnerName;
    public TMP_Text looserName;
    public TMP_Text looserCoinCount;
    public TMP_Text winnerCoinCount;



    private void Start()
    {
        collectablesSpawn.enabled = false; //stop spawn collectables when game just started
        waitingPanel.SetActive(true);
    }

    private void Update()
    {
        if (deathPanel.activeSelf)
        {
            collectablesSpawn.enabled = false; // stop spawn collectables when game was over
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2) //disable waiting panel if 2 players joined the room, and start spawning collectables
        {
            waitingPanel.SetActive(false);
            collectablesSpawn.enabled = true;
        }
    }

    public void RestartGame() //Disconnect player from server and load lobby scene
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Loading");
    }
}
