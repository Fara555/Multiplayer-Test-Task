using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Physics Ref:")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Main Parametrs")]
    [SerializeField] private float speed;

    [Header("UI Elements")]

    [HideInInspector] public PhotonView playerView;
    [HideInInspector] public string coinCurrentAmount;// current amount of coins of individual player(for RPC fucntion)
    private JoyStick joystick;

    private void Awake()//initiate components
    {
        joystick = FindObjectOfType<JoyStick>();
        playerView = GetComponent<PhotonView>();
    }

    private void Update() //set animator variables depending on joystick current vector
    {
        if (playerView.IsMine)
        {
            var dir = joystick.joystickVector;
            dir.Normalize();
            animator.SetFloat("Horizontal", dir.x); //transition between left(-1) or right(1)
            animator.SetFloat("Vertical", dir.y); //transition between up (1) or down (-1)
            animator.SetFloat("Speed", dir.sqrMagnitude); // transition between idling and running (less or greater than 0.1)
        }
    }

    private void FixedUpdate() //move player by adding velocity to him depending on joystick current vector
    {
        if (playerView.IsMine)
        {
            rb.velocity = new Vector3(joystick.joystickVector.x * speed, joystick.joystickVector.y * speed, rb.velocity.y); 
        }
    }
}
 