using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrictor : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;//speed of the game object
    double WPradius = 0.5;
    // Start is called before the first frame update
    void Update()
    {
        if (Vector2.Distance(waypoints[current].transform.position, transform.position) < WPradius)//distance between waypoint and player within radius
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);//moving the object towards the waypoint
        
    }
}
