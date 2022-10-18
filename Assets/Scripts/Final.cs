using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject WinLabel, LoseLabel;

    public void ActivateFinal(bool Win)
    {
        WinLabel.SetActive(Win);
        LoseLabel.SetActive(!Win);
        gameObject.SetActive(true);
    }
}
