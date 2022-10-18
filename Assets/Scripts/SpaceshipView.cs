using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class SpaceshipView : MonoBehaviour
{
    public ReactiveProperty<Vector2> Direction;
    public ReactiveProperty<int> HP;

    CompositeDisposable disposables = new CompositeDisposable();

    void Start()
    {
        Direction.Subscribe(_ => Direction = SpaceshipController.Instance.Direction).AddTo(disposables);
        HP.Subscribe(_ => { HP = SpaceshipModel.Instance.HP; }).AddTo(disposables);
    }
    // Здесь может быть реализована анимация и эффекты корабля, думаю, сейчас это не особо важно



    private void OnDisable()
    {
        disposables.Clear();
    }
}
