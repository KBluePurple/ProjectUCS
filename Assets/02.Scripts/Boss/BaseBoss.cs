using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : Entity {
    public Entity Target => _target;
    public BossStats BossStats => _bossStats;

    protected Entity _target;
    [SerializeField] protected List<Entity> _playerList = new List<Entity>();

    protected BossStats _bossStats;

    protected Rigidbody2D _rigidbody;
    protected Fsm _fsm;

    protected int _phaseIndex;
    protected float _movementSpeed;
    Vector2 _curDirection = Vector2.zero;


    protected Animator _animator;

    public override void Init() {
        base.Init();
        _healthSystem.Init(this);

        // Player로 수정해야 함
        Test[] playerComponents = FindObjectsOfType<Test>();
        _playerList = new List<Entity>(playerComponents);

        // 보스 스탯
        _bossStats = new BossStats(5000, 10000, 500, 200, 3, 0);


        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();


        _movementSpeed = _bossStats.MovementSpeed;
    }

    public void Move(Vector2 direction) {
        if (_curDirection != direction) {
            _curDirection = direction;
            _rigidbody.velocity = _curDirection * _movementSpeed;
            float x = _curDirection.x != 0 ? _curDirection.x : transform.localScale.x;
            transform.localScale = new Vector3(x, 1, 1);
        }
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
