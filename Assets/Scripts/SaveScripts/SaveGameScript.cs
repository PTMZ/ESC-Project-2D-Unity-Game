using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGameScript
{

    public static void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        //Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
        // Reset whatever is needed

        Debug.Log("Game Saved");
    }

    private static Save CreateSaveGameObject(){
        Save save = new Save();

        save.health = OfflineGameManager.instance.curHealth;
        save.maxHealth = OfflineGameManager.maxHealth;
        save.curAttack = OfflineGameManager.instance.curAttack;
        save.curScene = SceneManager.GetActiveScene().name;
        save.storyProgress = OfflineGameManager.instance.storyProgress;
        save.spawnPoints = OfflineGameManager.instance.spawnPoints;

        return save;
    }

    public static void LoadGame()
    { 
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // Update Game state
            OfflineGameManager.instance.curHealth = save.health;
            OfflineGameManager.maxHealth = save.maxHealth;
            OfflineGameManager.instance.curAttack = save.curAttack;
            OfflineGameManager.instance.storyProgress = save.storyProgress;
            OfflineGameManager.instance.spawnPoints = save.spawnPoints;

            AudioManager.instance.StopAll();
            SceneManager.LoadScene(save.curScene);
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
