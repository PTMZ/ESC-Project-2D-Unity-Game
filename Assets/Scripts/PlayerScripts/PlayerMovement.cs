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
    //private TriggerMelee melee;
    public GameObject meleeObject;
    public GameObject dashObject;
    
    Rigidbody2D rb2d;

    private bool prevButton = false;
    public float camSize = 4.0f;

    public OfflineGameManager OGMPrefab;
    // Start is called before the first frame update

    void Awake(){
        if(OfflineGameManager.instance == null){
            Instantiate(OGMPrefab);
        }
    }
    void Start()
    {
        float alpha = 0.5f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        dashObject.GetComponent<TrailRenderer>().colorGradient = gradient;
        //joystick = FindObjectOfType<Joystick>();
        button = FindObjectOfType<MyButton>();
        joystickMove = GameObject.Find("JoystickMove").GetComponent<Joystick>();
        joystickShoot = GameObject.Find("JoystickShoot").GetComponent<Joystick>();
        Debug.Log("joystickMove : " + joystickMove == null);
        Debug.Log("JoystickShoot : " + joystickShoot == null);

        cooldownTimeStamp = Time.time;
        dashTimeStamp = Time.time;
        offsetY = new Vector3(0,0.4f,0);

        myself = GetComponent<PlayerAvatar>();
        rb2d = GetComponent<Rigidbody2D>();
        //melee = GetComponentInChildren<TriggerMelee>();


        //offlineGM = FindObjectOfType<OfflineGameManager>();
        //offlineGM = OfflineGameManager.instance;
        Camera.main.orthographicSize = camSize;

        cooldown = OfflineGameManager.instance.curCooldown;
        Debug.Log("Player finished starting");
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
            cooldownTimeStamp = Time.time + OfflineGameManager.instance.curCooldown;
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
        rb2d.AddForce(myself.change * speed * OfflineGameManager.instance.moveMult);
        //rb2d.velocity = myself.change * speed * offlineGM.moveMult;
        //rb2d.velocity *= 0.9f;
    }

    void LateUpdate(){
        Vector3 upVector = new Vector3(0, 0, -1);
        //float camDistance = 10;
        Camera.main.orthographicSize = camSize;
        Camera.main.transform.position = transform.position + upVector;
    }

    void Shoot(){

        Vector3 bulletDir = new Vector3(joystickShoot.Horizontal, joystickShoot.Vertical, 0).normalized;
        Debug.Log("is shooting");
        if(SceneManager.GetActiveScene().name == "MultiPlayer"){
            OnlineGameManager.SpawnBullet(transform.position + offsetY + bulletDir*myRadius, transform.rotation, bulletDir);
        }
        else{
            //GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + offsetY + bulletDir*myRadius, transform.rotation);
            //bulletInstance.GetComponent<BulletScript>().isOnline = false;
            //bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpd;
            AudioManager.instance.Play("CookieRange");
            OfflineGameManager.instance.SpawnBullet(transform.position + offsetY + bulletDir*myRadius, transform.rotation, bulletDir);
        }
        rb2d.AddForce(bulletDir * -1 * recoil);

    }

    void Dash(){
        //dashRenderer.renderer.material.SetColor("_TintColor", new Color(1, 1, 1, 0.5f));

        if(Time.time > dashTimeStamp){
            //Debug.Log("DASH");
            //Vector3 upVector = new Vector3(0, 1, 0);
            //rb2d.AddForce(upVector * dashSpeed);
            AudioManager.instance.Play("CookieMelee");
            rb2d.AddForce(myself.change * dashSpeed);
            dashTimeStamp = Time.time + dashCooldown;

            //melee.startMelee();
            meleeObject.SetActive(true);
        }
    }
}
