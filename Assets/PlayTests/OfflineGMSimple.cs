using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineGMSimple
{

    public static OfflineGMSimple instance;

    public float curHealth;
    public float maxHealth;
    public float curDamage;
    public float curBulletSpeed;
    public float curCooldown;
    public int storyProgress;

    [HideInInspector]
    public float moveMult;
    [HideInInspector]
    public float dmgMult;

    private OfflineGMSimple(){
        maxHealth = 1;
        curHealth = maxHealth;
        curDamage = 10;
        curBulletSpeed = 10;
        curCooldown = 1;
        storyProgress = 0;

        moveMult = 1;
        dmgMult = 1;
    }

    public static void CreateSingleton(){
        if(instance == null){
            instance = new OfflineGMSimple();
        }
    }
    
}
