using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Rocket : MonoBehaviour
{
    CompositeDisposable disposables = new CompositeDisposable();
    public float Speed;

    private void Awake()
    {
        Observable.EveryFixedUpdate()
            .Subscribe(x => {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
            }).AddTo(disposables);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Spaceship = other.GetComponent<Asteroid>();
        if (Spaceship != null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            PostDestroyEffects();
        }

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }
    public void PostDestroyEffects()
    {

    }
}
