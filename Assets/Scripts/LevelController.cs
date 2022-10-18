using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    public GameController gameController;

    public Level[] levels;
    public GameObject[] Obstacles;
    public GameObject Menu;
    public int LevelsCount;
    public int ObstaclesCount;
    public float MaxTimeBetweenSpawn, SpawnRadius;
    public bool ForcedCreateNew;

    
    void Awake()
    {
        Instance = this;

        BinaryFormatter formatter = new BinaryFormatter();
        levels = new Level[LevelsCount];
        if (File.Exists("Level" + (LevelsCount - 1) + ".xml") && !ForcedCreateNew)
        {
            for(int i = 0; i < LevelsCount; i++)
            {
                Stream stream = File.Open("Level" + i + ".xml", FileMode.Open);
                levels[i] = (Level)formatter.Deserialize(stream);
                stream.Close();
            }
        }
        else
        {
            for(int i = 0; i < LevelsCount; i++)
            {
                levels[i] = new Level(ObstaclesCount, Obstacles.Length, MaxTimeBetweenSpawn, SpawnRadius);
                Stream stream = File.Open("Level" + i + ".xml", FileMode.Create);
                formatter.Serialize(stream, levels[i]);
                stream.Close();
            }
        }
    }

    public void StartLevel(int LevelNumber)
    {
        Menu.SetActive(false);
        gameController.PlayLevel(LevelNumber, levels[LevelNumber], Obstacles);
    }


    
}
[Serializable()]
public class Level
{
    public int[] Obstacles;
    public float[] TimeBetweenObstacles;
    public float[] ObstaclesSpawnPosition;

    public Level(int ObstaclesCount, int ObstacleTypesCount, float MaxTimeBetweenSpawn, float SpawnRadius)
    {
        Obstacles = new int[UnityEngine.Random.Range(ObstaclesCount, ObstaclesCount * 2)];
        TimeBetweenObstacles = new float[Obstacles.Length];
        ObstaclesSpawnPosition = new float[Obstacles.Length];

        for (int i = 0; i < Obstacles.Length; i++)
        {
            Obstacles[i] = UnityEngine.Random.Range(0, ObstacleTypesCount);
            TimeBetweenObstacles[i] = UnityEngine.Random.value * MaxTimeBetweenSpawn;
            ObstaclesSpawnPosition[i] = UnityEngine.Random.value * SpawnRadius * 2 - SpawnRadius;
        }
    }
}
