using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : Entity {
    public Entity Target => _target;

    protected HealthSystem _healthSystem;
    protected Rigidbody2D _rigidbody;
    protected Fsm _fsm;

    protected int _phaseIndex;
    protected float _moveSpeed = 3f;

    protected Entity _target;
    [SerializeField] protected List<Entity> _playerList = new List<Entity>();


    protected void Awake() {
        Init();
    }

    public void Init() {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.Init();
        _rigidbody = GetComponent<Rigidbody2D>();

        // Player로 수정해야 함
        Test[] playerComponents = FindObjectsOfType<Test>();
        _playerList = new List<Entity>(playerComponents);
    }

    public override void Move(Vector2 direction) {
        _rigidbody.velocity = direction * _moveSpeed;
    }

    public Entity FindClosestPlayer() {
        float closestDistance = Mathf.Infinity;
        Entity closestTarget = null;

        foreach (Entity player in _playerList) {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestTarget = player;
            }
        }

        _target = closestTarget;

        return closestTarget;
    }
}
