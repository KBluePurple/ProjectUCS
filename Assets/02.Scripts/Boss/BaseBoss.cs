using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : Entity {
    public Entity Target => _target;
    public BossStats BossStats => _bossStats;
    public Animator Animator => _animator;

    protected Entity _target;
    [SerializeField] protected List<Entity> _playerList = new List<Entity>();

    protected BossStats _bossStats;

    protected Rigidbody2D _rigidbody;
    protected Fsm _fsm;

    protected int _phaseIndex;
    protected float _moveSpeed;
    private Vector2 _curDirection = Vector2.zero;
    private float _curX;

    protected CooldownTimer _findTimer;

    protected Animator _animator;

    public override void Init() {
        base.Init();

        // Player로 수정해야 함
        Test[] playerComponents = FindObjectsOfType<Test>();
        _playerList = new List<Entity>(playerComponents);

        // 보스 스탯
        _bossStats = new BossStats(5000, 10000, 500, 200, 3, 0, 0);

        _healthSystem.Init(this, _bossStats.Hp);


        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _moveSpeed = _bossStats.MoveSpeed;

        _findTimer = new CooldownTimer(2f);
    }

    public void Move(Vector2 direction) {
        if (_curDirection != direction) {
            _curDirection = direction;
            _rigidbody.velocity = _curDirection * _moveSpeed;

            if (_curDirection.x != 0)
                Look(_curDirection.x);
        }
    }

    public void Look(float x) {
        if (_curX == x) {
            return;
        }

        _curX = x;
        transform.localScale = new Vector3(_curX, 1, 1);
    }

    public void LookTarget() {
        if (Target != null) {
            Vector2 direction = Target.transform.position - transform.position;
            direction.Normalize();

            if (direction.x != 0)
                Look(direction.x);
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

    public Entity FindRandomPlayer() {
        if (_findTimer.IsCooldownReady()) {
            int randomIndex = Random.Range(0, _playerList.Count);
            _target = _playerList[randomIndex];
            _findTimer.StartCooldown();
        }
        else {
            _findTimer?.UpdateTimer();
        }
        return _target;
    }

    public void RemovePlayerList(Entity player) {
        _playerList.Remove(player);
    }
}
