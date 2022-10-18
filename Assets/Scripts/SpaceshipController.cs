using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class SpaceshipController : MonoBehaviour
{
    public static SpaceshipController Instance { get; private set; }
    public ReactiveProperty<Vector2> Direction = new ReactiveProperty<Vector2>();
    public ReactiveProperty<int> HP = new ReactiveProperty<int>();
    public Joystick jstck;
    public Vector2 MinPoint, MaxPoint;

    CompositeDisposable disposables = new CompositeDisposable();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Direction.Subscribe(x => { });


        HP.Subscribe(x => {
            HP = SpaceshipModel.Instance.HP;
        }).AddTo(disposables);


        HP.Where(hp => hp <= 0)
            .Subscribe(hp => {
                DisableController();
            }).AddTo(disposables);

            Observable.EveryFixedUpdate()
            .RepeatUntilDisable(this)
            .Subscribe(dir =>
            {
                Vector2 newDirection = jstck.Direction;
                if (transform.position.x <= MinPoint.x && newDirection.x < 0)
                {
                    newDirection.x = MinPoint.x - transform.position.x;
                }
                if (transform.position.x >= MaxPoint.x && newDirection.x > 0)
                {
                    newDirection.x = MaxPoint.x - transform.position.x;
                }
                if (transform.position.z <= MinPoint.y && newDirection.y < 0)
                {
                    newDirection.y = MinPoint.y - transform.position.z;
                }
                if (transform.position.z >= MaxPoint.y && newDirection.y > 0)
                {
                    newDirection.y = MaxPoint.y - transform.position.z;
                }
                Direction.Value = newDirection;
            })
            .AddTo(disposables);
    }


    public void DisableController()
    {
        Direction.Value = Vector2.zero;
        jstck.gameObject.SetActive(false);
        enabled = false;
    }


    private void OnDisable()
    {
        disposables.Clear();
    }
}
