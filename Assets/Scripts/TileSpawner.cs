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
    public float xOffset = 7.5f;
    public float yOffset = 5.0f;


    void Start()
    {
        // Put file in Assets/Resources
        TextAsset textFile = (TextAsset)Resources.Load(fileName);
        string info = textFile.text;
        string[] lines = info.Split('\n');
        for(int i=0; i<rows; i++){
            string[] vals = lines[i].Split(',');
            for(int j=0; j<columns; j++){
                int val = int.Parse(vals[j]);
                Vector3 spawnPos = new Vector3(j*tileWidth-xOffset,i*tileWidth-yOffset,0);
                if(val != -1){
                    Instantiate(TilePrefabs[val], spawnPos, Quaternion.identity);
                }

            }
        }
    }
}
