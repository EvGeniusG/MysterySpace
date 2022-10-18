using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int LevelNumber;
    public GameObject Lock;
    public Text Label;
    public Image ButtonImage;
    public Color CompletedColor;

    bool Locked;
    private void OnEnable()
    {
        Label.text = LevelNumber.ToString();

        if (PlayerPrefs.HasKey("LevelCompleted" + (LevelNumber - 2)) || LevelNumber == 1)
        {
            Locked = false;
            Lock.SetActive(false);
            Label.gameObject.SetActive(true);
            if(PlayerPrefs.HasKey("LevelCompleted" + (LevelNumber - 1)))
                ButtonImage.color = CompletedColor;

        }
        else
        {
            Locked = true; ;
            Lock.SetActive(true);
            Label.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        if(!Locked)
            LevelController.Instance.StartLevel(LevelNumber - 1);
    }
}
