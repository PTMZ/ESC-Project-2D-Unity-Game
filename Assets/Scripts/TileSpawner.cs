using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int columns;
    public int rows;
    public float tileWidth;
    public string fileName;
    public GameObject[] TilePrefabs;
    public Transform topLeft;


    void Start()
    {
        // Put file in Assets/Resources
        //Vector3 offset = new Vector3(xOffset, yOffset, 0);
        TextAsset textFile = (TextAsset)Resources.Load(fileName);
        string info = textFile.text;
        string[] lines = info.Split('\n');
        for(int i=0; i<rows; i++){
            string[] vals = lines[i].Split(',');
            for(int j=0; j<columns; j++){
                int val = int.Parse(vals[j]);
                if(val != -1){
                    Vector3 spawnPos = new Vector3(j * tileWidth, -i * tileWidth, 0);
                    Instantiate(TilePrefabs[val], spawnPos + topLeft.position, Quaternion.identity);
                }
               

            }
        }
    }
}
