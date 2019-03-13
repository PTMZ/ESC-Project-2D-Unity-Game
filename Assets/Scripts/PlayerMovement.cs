using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb2d;
    protected Joystick joystick;
    protected MyButton button;
    public float speed;
    public float myRadius;

    public GameObject bulletPrefab;
    
    public float DeathTime = 1;
    public float bulletSpd;
    public float recoil;

    private bool prevButton = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
        button = FindObjectOfType<MyButton>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;

        // tilt = Quaternion.Euler(90, 0, 0) * tilt;

        rb2d.AddForce(tilt * speed);

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
    }

    void LateUpdate(){
        Vector3 upVector = new Vector3(0, 0, -1);
        float camDistance = 10;
        Camera.main.transform.position = transform.position + upVector*camDistance;
    }

    void Shoot(){
        Vector3 upVector = new Vector3(0, 1, 0);
        Vector3 bulletDir = (joystick.Horizontal==0 && joystick.Vertical==0) ? upVector : new Vector3(joystick.Horizontal, joystick.Vertical, 0).normalized;
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position + bulletDir*myRadius, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpd;

        rb2d.AddForce(bulletDir * -1 * recoil);
        //Debug.Log(bulletInstance.name);
        Destroy(bulletInstance,DeathTime);
    }
}
