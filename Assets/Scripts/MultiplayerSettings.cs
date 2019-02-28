using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour
{

    public static MultiplayerSettings multiplayerSettings;

    public bool delayStart;
    public int maxPlayers;

    public int menuScene;
    public int multiplayerScene;


    private void Awake(){
        if(MultiplayerSettings.multiplayerSettings == null){
            MultiplayerSettings.multiplayerSettings = this;
        } else {
            if(MultiplayerSettings.multiplayerSettings != this){
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
