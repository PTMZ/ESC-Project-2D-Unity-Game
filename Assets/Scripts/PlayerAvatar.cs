using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAvatar : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public InputStr Input;
    public struct InputStr
    {
        public float moveX;
        public float moveY;
    }

    public const float speed = 10;

    protected Rigidbody2D rb2d;

    private void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2d == null){
            return;
        }
        // Animate stuff here
    }

    void FixedUpdate()
    {
        if(rb2d == null){
            return;
        }
        rb2d.AddForce(new Vector3(Input.moveX, Input.moveY, 0));

    }
}
