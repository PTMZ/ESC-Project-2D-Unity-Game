using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCalc : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Vector3 change; 

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        rb2d.AddForce(change);
    }
}
