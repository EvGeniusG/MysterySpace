using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Asteroid : MonoBehaviour
{
    CompositeDisposable disposables = new CompositeDisposable();
    public float Speed, MinZPosition;

    private void Awake()
    {
        Observable.EveryFixedUpdate()
            .Subscribe(x => {
                transform.Translate(Vector3.back * Speed * Time.deltaTime, Space.World);
                if (transform.position.z < MinZPosition)
                {
                    Destroy(gameObject);
                }
            }).AddTo(disposables);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Spaceship = other.GetComponent<SpaceshipModel>();
        if (Spaceship != null)
        {
            Destroy(gameObject);
            Spaceship.Damage();
            PostDestroyEffects();
        }
        
    }


    private void OnDisable()
    {
        disposables.Clear();
    }
    public void PostDestroyEffects()
    {

    }
}
