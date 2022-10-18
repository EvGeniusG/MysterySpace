using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class GameController : MonoBehaviour
{
    CompositeDisposable disposables = new CompositeDisposable();
    public ReactiveProperty<int> HP = new ReactiveProperty<int>(3);
    public GameObject GamePlayCanvas;
    public float TimeBeforeObstacles;
    public Final final;
    int LevelNumber;

    private void Start()
    {
        HP.Subscribe(x => {
            HP = SpaceshipModel.Instance.HP;
        }).AddTo(disposables);

        HP.Where(hp => hp <= 0)
            .Subscribe(x =>
            {
                EndLevel();
            }).AddTo(disposables);

    }

    public void PlayLevel(int levelNumber, Level level, GameObject[] Obstacles)
    {
        LevelNumber = levelNumber;
        GamePlayCanvas.SetActive(true);
        StartCoroutine(AsyncPlayLevel(level, Obstacles));
    }

    IEnumerator AsyncPlayLevel(Level level, GameObject[] Obstacles)
    {
        yield return new WaitForSeconds(TimeBeforeObstacles);
        foreach(var rl in FindObjectsOfType<RocketLauncher>())
        {
            rl.StartRocketLaunch();
        }
        for (int i = 0; i < level.Obstacles.Length; i++)
        {
            GameObject obstacle = Instantiate(Obstacles[level.Obstacles[i]], transform.position, Obstacles[level.Obstacles[i]].transform.rotation);
            obstacle.transform.Translate(Vector3.right * level.ObstaclesSpawnPosition[i]);
            yield return new WaitForSeconds(level.TimeBetweenObstacles[i]);
        }
        while (FindObjectsOfType<Asteroid>().Length > 0)
        {
            yield return new WaitForSeconds(1);
        }
        if(HP.Value > 0)
        {
            PlayerPrefs.SetInt("LevelCompleted" + LevelNumber, 1);
            EndLevel();
        }
            
    }

    void EndLevel()
    {
        foreach (var rl in FindObjectsOfType<RocketLauncher>())
        {
            rl.EndRocketLaunch();
        }
        SpaceshipController.Instance.DisableController();
        final.ActivateFinal(HP.Value > 0);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }
}
