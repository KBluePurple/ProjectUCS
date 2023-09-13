using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : Entity
{
    //public Entity Target => _target;
    public GameObject Target => _target;
    public BossStats BossStats => _bossStats;
    public Animator Animator => _animator;

    //[SerializeField] protected Entity _target;
    [SerializeField] protected GameObject _target;
    [SerializeField] protected List<GameObject> _playerList = new List<GameObject>();

    protected BossStats _bossStats;

    protected Rigidbody2D _rigidbody;
    protected Fsm _fsm;

    protected int _phaseIndex;
    protected float _moveSpeed;
    private Vector2 _curDirection = Vector2.zero;
    private Vector2 _curScale = Vector2.one;
    private float _curX = 1;

    protected CooldownTimer _findTimer;

    protected Animator _animator;


    protected BossHealthBar _bossHealthBar;


    public override void Init()
    {
        base.Init();

        // Player로 수정해야 함
        //Test[] playerComponents = FindObjectsOfType<Test>();
        //_playerList = new List<Entity>(playerComponents);


        _healthSystem.Init(this, _bossStats.Hp);


        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _moveSpeed = _bossStats.MoveSpeed;

        _findTimer = new CooldownTimer(2f);

        _bossHealthBar = FindObjectOfType<BossHealthBar>();
    }

    public void Move(Vector2 direction)
    {
        if (_curDirection != direction)
        {
            _curDirection = direction;
            _rigidbody.velocity = _curDirection * _moveSpeed;

            if (_curDirection.x != 0)
                Look(_curDirection.x > 0 ? 1 : -1);
        }
    }

    public void Look(float x)
    {
        if (_curX == x)
        {
            return;
        }

        _curX = x;
        // TODO: 수정해야 함
        transform.localScale = new Vector3(_curX * _curScale.x, _curScale.y, 1);
    }

    public void LookTarget(Entity target = null)
    {
        if (Target != null)
        {
            Vector2 direction = Target.transform.position - transform.position;
            direction.Normalize();

            if (direction.x != 0)
            {
                Look(direction.x > 0 ? 1 : -1);
            }
        }
    }

    public virtual void SetScale(Vector2 scale)
    {
        if (_curScale == scale)
            return;

        _curScale = scale;
        transform.localScale = new Vector3(_curX * _curScale.x, _curScale.y, 1);
    }


    public GameObject FindClosestPlayer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestTarget = null;

        foreach (GameObject player in _playerList)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = player;
            }
        }

        _target = closestTarget;

        return _target;
    }

    public GameObject FindRandomPlayer()
    {
        if (_findTimer.IsCooldownReady())
        {
            //if (_playerList.Count <= 0)
            //{
            //    // TODO: 남은 플레이어 존재하지 않음
            //    _target = null;
            //    return null;
            //}

            int randomIndex = Random.Range(0, _playerList.Count);
            _target = _playerList[randomIndex].gameObject;
            _findTimer.StartCooldown();
        }
        else
        {
            _findTimer?.UpdateTimer();
        }

        return _target;
    }

    public void RemovePlayerList(GameObject player)
    {
        _playerList.Remove(player);
    }

    public void UpdateHealthBar()
    {
        _bossHealthBar.UpdateHealthBar(_healthSystem.Health, _bossStats.Hp);
    }
}
