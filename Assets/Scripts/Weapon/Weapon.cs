using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [Header("Main Ref:")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform weaponTransform;

    [Header("Main Parametrs")]
    [SerializeField] private float fireForce;

    private JoyStick joystick;
    private PhotonView view;
    private EventTrigger eventTrigger;
    private EventTrigger.Entry entry;


    private void Start() //assign components and Click on fire button event
    {
        joystick = FindObjectOfType<JoyStick>();
        view = GetComponent<PhotonView>();
        eventTrigger = GameObject.Find("UICanvas/FireButton").GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick; // Select event type
        entry.callback.AddListener((data) => { Fire((PointerEventData)data); }); //call method when button was clicked
        if (view.IsMine)
        {
            eventTrigger.triggers.Add(entry);//Add new event to event trigger system for indiviual players
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            RotateGun(); // Rotate gun on on indiviual player
        }
    }
 
    public void Fire(PointerEventData data) //initiate bullets in network and add force to them
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, weaponTransform.rotation); //initiate bullet of fire point position
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse); // add force to the bullet  
    }

    void RotateGun() // rotate gun to the joystick current direction
    {
        if (view.IsMine)
        {
            if (joystick.joystickVector == Vector3.zero) return; //return zero if joystick stay intact
            float angle = Mathf.Atan2(joystick.joystickVector.y, joystick.joystickVector.x) * Mathf.Rad2Deg; //calculate angle for rotation
            var lookRotation = Quaternion.Euler(angle * Vector3.forward); //rotate around calculated angle
            weaponTransform.rotation = lookRotation;
        }
    }
}
