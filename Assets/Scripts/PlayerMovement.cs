using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb2d;
    protected Joystick joystickMove;
    protected Joystick joystickShoot;
    //protected MyButton button;
    public float speed;
    public float myRadius;

    public GameObject bulletPrefab;
    
    public float DeathTime = 1;
    public float bulletSpd;
    public float recoil;

    private float cooldownTimeStamp;
    public float cooldown = 0.2f;
    Vector3 offsetY;
    private PlayerAvatar myself;
    //private bool prevButton = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //joystick = FindObjectOfType<Joystick>();
        //button = FindObjectOfType<MyButton>();
        joystickMove = GameObject.Find("JoystickMove").GetComponent<Joystick>();
        joystickShoot = GameObject.Find("JoystickShoot").GetComponent<Joystick>();
        cooldownTimeStamp = Time.time;
        offsetY = new Vector3(0,1.5f,0);

        myself = GetComponent<PlayerAvatar>();
        //Camera.main.orthographicSize = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 tilt = Input.acceleration;

        // tilt = Quaternion.Euler(90, 0, 0) * tilt;
        if(Application.isEditor){
            myself.change = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        else{
            myself.change = new Vector3(joystickMove.Horizontal, joystickMove.Vertical, 0);
        }

        //Debug.Log(new Vector2(joystickShoot.Horizontal, joystickShoot.Vertical).magnitude);
        if(Time.time > cooldownTimeStamp && new Vector2(joystickShoot.Horizontal, joystickShoot.Vertical).magnitude > 0.5){
            cooldownTimeStamp = Time.time + cooldown;
            Shoot();
        }

        /* 
        if(!button.pressed){
            if(prevButton){
                prevButton = false;
                Shoot();
                //Debug.Log("Button release");
            }
        }
        else{
            prevButton = true;
        }
        */
    }

    void FixedUpdate(){
        rb2d.AddForce(myself.change * speed);
    }

    void LateUpdate(){
        Vector3 upVector = new Vector3(0, 0, -1);
        float camDistance = 10;
        Camera.main.transform.position = transform.position + upVector*camDistance;
    }

    void Shoot(){
        //Vector3 upVector = new Vector3(0, 1, 0);
        //Vector3 bulletDir = (joystick.Horizontal==0 && joystick.Vertical==0) ? upVector : new Vector3(joystick.Horizontal, joystick.Vertical, 0).normalized;
        /*
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + bulletDir*myRadius, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpd;
        rb2d.AddForce(bulletDir * -1 * recoil);
        Destroy(bulletInstance,DeathTime);
        */
        //GameManager.SpawnBullet(transform.position + bulletDir*myRadius, transform.rotation, bulletDir * bulletSpd, DeathTime);
        //rb2d.AddForce(bulletDir * -1 * recoil);

        Vector3 bulletDir = new Vector3(joystickShoot.Horizontal, joystickShoot.Vertical, 0).normalized;
        GameManager.SpawnBullet(transform.position + offsetY + bulletDir*myRadius, transform.rotation, bulletDir * bulletSpd, DeathTime);
        rb2d.AddForce(bulletDir * -1 * recoil);

    }
}
