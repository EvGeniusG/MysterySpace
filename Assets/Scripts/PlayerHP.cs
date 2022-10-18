using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class PlayerHP : MonoBehaviour
{
    public Text Table;
    
    CompositeDisposable disposables = new CompositeDisposable();
    void Start()
    {
        SpaceshipModel.Instance.HP.SubscribeToText(Table).AddTo(disposables);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }
}
