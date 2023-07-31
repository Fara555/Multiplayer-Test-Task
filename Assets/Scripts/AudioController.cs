using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameController gameController;
    [Header("Sounds")]
    [SerializeField] private AudioSource mainMusic;
    public AudioSource enviromentHitSound;
    public AudioSource playerHitSound;
    public AudioSource coinPickUpSound;

    void Update()
    {
        if (gameController.waitingPanel.activeSelf)
        {
            mainMusic.Pause();
        }
        else
        {
            mainMusic.UnPause();
        }
        if (gameController.deathPanel.activeSelf)
        {
            mainMusic.Pause();
        }
        
    }
}
