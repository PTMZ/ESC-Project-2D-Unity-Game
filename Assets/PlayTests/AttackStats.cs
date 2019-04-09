using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStats : MonoBehaviour
{
    public int type;
    public int playerid;

    public float damage;
    public float bulletSpeed;
    public float deathTime;
    public float cooldown;

    public Transform followMelee;
    public Vector3 dir;
    public GameObject onDeathSpawn;


}
