using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpaceshipModel : MonoBehaviour
{
    public static SpaceshipModel Instance { get; private set; }

    public ReactiveProperty<int> HP = new ReactiveProperty<int>(3);
    public ReactiveProperty<Vector2> Direction = new ReactiveProperty<Vector2>(new Vector2());
    Rigidbody rb;
    public float Speed;
    CompositeDisposable disposables = new CompositeDisposable();
    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        HP
            .Where(hp => hp <= 0)
            .Subscribe(hp =>
            {
                DestroyShip();
            }).AddTo(disposables);

       
    }
    private void Start()
    {
        transform.ObserveEveryValueChanged(x => SpaceshipController.Instance.Direction)
            .RepeatUntilDisable(this)
            .Subscribe(direction => {
            Direction = SpaceshipController.Instance.Direction;
        }).AddTo(disposables);
    
    }


    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + new Vector3(Direction.Value.x, 0, Direction.Value.y) * Speed * Time.fixedDeltaTime);
    }
    public void Damage()
    {
        HP.Value--;
    }

    void DestroyShip()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }
}
