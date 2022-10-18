using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UniRx;
public class RocketLauncher : MonoBehaviour
{
    public GameObject Rocket;
    public float TimeBetweenLaunches;
    
    CompositeDisposable disposables = new CompositeDisposable();

    public void StartRocketLaunch()
    {
        Observable.Interval(TimeSpan.FromSeconds(TimeBetweenLaunches))
            .RepeatUntilDisable(this)
            .Subscribe(x => {
                Instantiate(Rocket, transform.position, transform.rotation);
            }).AddTo(disposables);
    }

    public void EndRocketLaunch()
    {
        disposables.Clear();
    }

    private void OnDisable()
    {
        disposables.Clear();
    }
}
