using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int levelID;
    public GameObject[] levelPrefabs;
    private static LevelManager instance;

    public static LevelManager Instance { get => instance; set => instance = value; }
    public int LevelID { get => levelID; set => levelID = value; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        levelID = PlayerPrefs.GetInt("Level", 0);
        if (levelID < 2)
        {
            Instantiate(levelPrefabs[levelID]); //spawns the level prefab here
        }

        else
        {
            levelID = 0;
            Instantiate(levelPrefabs[levelID]);
        }
    }
   
        public void SetLevel()
    {
        levelID++;
        PlayerPrefs.SetInt("Level", levelID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloads the scene
    }
}
