using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float health;
    public float maxHealth;
    public int curAttack;
    public string curScene;
    public int storyProgress;
    public Dictionary<string, int> spawnPoints;
}