using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    protected Joystick joystickMove;
    protected Joystick joystickShoot;
    protected MyButton button;

    public float speed;
    public float dashSpeed;
    public float myRadius;
    
    public float recoil;

    private float cooldownTimeStamp;
    public float cooldown;
    private float dashTimeStamp;
    public float dashCooldown = 0.5f;

    Vector3 offsetY;
    private PlayerAvatar myself;
    private TriggerMelee melee;
    Rigidbody2D rb2d;

    private bool prevButton = false;
    public float camSize = 4.0f;

    private OfflineGameManager offlineGM;
    public OfflineGameManager OGMPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //joystick = FindObjectOfType<Joystick>();
        button = FindObjectOfType<MyButton>();
        joystickMove = GameObject.Find("JoystickMove").GetComponent<Joystick>();
        joystickShoot = GameObject.Find("JoystickShoot").GetComponent<Joystick>();

        cooldownTimeStamp = Time.time;
        dashTimeStamp = Time.time;
        offsetY = new Vector3(0,1.5f,0);

        myself = GetComponent<PlayerAvatar>();
        rb2d = GetComponent<Rigidbody2D>();
        melee = GetComponentInChildren<TriggerMelee>();

        //offlineGM = FindObjectOfType<OfflineGameManager>();
        if(OfflineGameManager.instance == null){
            Instantiate(OGMPrefab);
        }
        offlineGM = OfflineGameManager.instance;
        Camera.main.orthographicSize = camSize;

        cooldown = offlineGM.curCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 tilt = Input.acceleration;

        // tilt = Quaternion.Euler(90, 0, 0) * tilt;
        if(Application.isEditor){
            myself.change = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        }
        else{
            myself.change = new Vector3(joystickMove.Horizontal, joystickMove.Vertical, 0).normalized;
        }

        //Debug.Log(new Vector2(joystickShoot.Horizontal, joystickShoot.Vertical).magnitude);
        if(Time.time > cooldownTimeStamp && new Vector2(joystickShoot.Horizontal, joystickShoot.Vertical).magnitude > 0.5){
            cooldownTimeStamp = Time.time + offlineGM.curCooldown;
            Shoot();
        }

        
        if(!button.pressed){
            if(prevButton){
                prevButton = false;
                //Debug.Log("Button release");
                Dash();
            }
        }
        else{
            prevButton = true;
        }
        
    }

    void FixedUpdate(){
        rb2d.AddForce(myself.change * speed * offlineGM.moveMult);
    }

    void LateUpdate(){
        Vector3 upVector = new Vector3(0, 0, -1);
        float camDistance = 10;
        Camera.main.transform.position = transform.position + upVector*camDistance;
    }

    void Shoot(){

        Vector3 bulletDir = new Vector3(joystickShoot.Horizontal, joystickShoot.Vertical, 0).normalized;
        if(SceneManager.GetActiveScene().name == "MultiPlayer"){
            GameManager.SpawnBullet(transform.position + offsetY + bulletDir*myRadius, transform.rotation, bulletDir);
        }
        else{
            //GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + offsetY + bulletDir*myRadius, transform.rotation);
            //bulletInstance.GetComponent<BulletScript>().isOnline = false;
            //bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpd;
            AudioManager.instance.Play("CookieRange");
            offlineGM.SpawnBullet(transform.position + offsetY + bulletDir*myRadius, transform.rotation, bulletDir);
        }
        rb2d.AddForce(bulletDir * -1 * recoil);

    }

    void Dash(){
        if(Time.time > dashTimeStamp){
            //Debug.Log("DASH");
            //Vector3 upVector = new Vector3(0, 1, 0);
            //rb2d.AddForce(upVector * dashSpeed);
            AudioManager.instance.Play("CookieMelee");
            rb2d.AddForce(myself.change * dashSpeed);
            dashTimeStamp = Time.time + dashCooldown;

            melee.startMelee();
        }
    }
}
