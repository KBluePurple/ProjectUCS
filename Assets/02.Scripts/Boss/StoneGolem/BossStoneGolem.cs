using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BossStoneGolemEventType {
    Gigantic,
    DanDanMukZic,
    Die
}

public class BossStoneGolem : BaseBoss, IListener {
    public enum StateType {
        Idle,
        Move,
        AttackA, // 근접 공격
        AttackB, // 원거리 공격
        AttackC, // 레이저 공격
        Heal, // 방어 태세
        Die,
        Gigantic,  // 2페이즈
        DanDanMukZic // 3페이즈
    }

    public StoneGolemHealthSystem StoneGolemHealthSystem => _healthSystem as StoneGolemHealthSystem;


    public CooldownTimer MoveCooldownTimer => _moveCooldownTimer;
    public StoneGolemAttackACondition AttackACondition => _attackACondition;
    public StoneGolemAttackBCondition AttackBCondition => _attackBCondition;
    public StoneGolemAttackCCondition AttackCCondition => _attackCCondition;
    public StoneGolemHealCondition HealCondition => _healCondition;


    public StoneGolemAttackA StoneGolemAttackA => _stoneGolemAttackA;
    public StoneGolemAttackB StoneGolemAttackB => _stoneGolemAttackB;


    protected CooldownTimer _moveCooldownTimer;
    protected StoneGolemAttackACondition _attackACondition;
    protected StoneGolemAttackBCondition _attackBCondition;
    protected StoneGolemAttackCCondition _attackCCondition;
    protected StoneGolemHealCondition _healCondition;


    protected StateType _curState;
    protected StateType _nextState;

    protected StoneGolemAttackA _stoneGolemAttackA;
    protected StoneGolemAttackB _stoneGolemAttackB;


    private void Start() {
        _curState = _nextState = StateType.Idle;
        _phaseIndex = 1;

        _fsm = new Fsm(new StoneGolemIdleState(this));

        _moveCooldownTimer = new CooldownTimer(4f);
        _attackACondition = new StoneGolemAttackACondition(this, 3f);
        _attackBCondition = new StoneGolemAttackBCondition(this, 8f);
        _attackCCondition = new StoneGolemAttackCCondition(this, 13f);
        _healCondition = new StoneGolemHealCondition(this, 15f);

        _stoneGolemAttackA = GetComponentInChildren<StoneGolemAttackA>();
        _stoneGolemAttackA.Init(this);

        _stoneGolemAttackB = GetComponentInChildren<StoneGolemAttackB>();
        StoneGolemAttackB.Init(this);

        EventManager.Instance.AddListener(BossStoneGolemEventType.Gigantic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.DanDanMukZic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.Die, this);
    }

    private void Update() {
        // Test
        if (InputSystem.GetDevice<Keyboard>().qKey.wasPressedThisFrame)
            _healthSystem.TakeDamage(1000);


        if (_curState == StateType.Idle) {
            _moveCooldownTimer?.UpdateTimer();
        }


        if (_curState != _nextState) {
            _curState = _nextState;

            switch (_curState) {
                case StateType.Idle:
                    _fsm.ChangeState(new StoneGolemIdleState(this));
                    break;
                case StateType.Move:
                    _fsm.ChangeState(new StoneGolemMoveState(this));
                    break;
                case StateType.AttackA:
                    _fsm.ChangeState(new StoneGolemAttackAState(this));
                    break;
                case StateType.AttackB:
                    _fsm.ChangeState(new StoneGolemAttackBState(this));
                    break;
                case StateType.AttackC:
                    _fsm.ChangeState(new StoneGolemAttackCState(this));
                    break;
                case StateType.Heal:
                    _fsm.ChangeState(new StoneGolemHealState(this));
                    break;
                case StateType.Die:
                    _fsm.ChangeState(new StoneGolemDieState(this));
                    break;
                case StateType.Gigantic:
                    _fsm.ChangeState(new StoneGolemGiganticState(this));
                    break;
                case StateType.DanDanMukZic:
                    _fsm.ChangeState(new StoneGolemDanDanMukZicState(this));
                    break;
            }
        }

        _fsm?.UpdateState();
    }

    private void LateUpdate() {
        // FindClosestPlayer();
        FindRandomPlayer();
        CheckTransitionToNextFhase();
    }

    public void SetNextState(StateType state) {
        _nextState = state;
    }

    private void CheckTransitionToNextFhase() {
        float value = StoneGolemHealthSystem.GetNormalizedHealth();
        if (_phaseIndex == 1 && value < 0.6f) {
            _phaseIndex = 2;
            EventManager.Instance.PostNotification(BossStoneGolemEventType.Gigantic, this, null);
            return;
        }

        if (_phaseIndex == 2 && value < 0.3f) {
            _phaseIndex = 3;
            EventManager.Instance.PostNotification(BossStoneGolemEventType.DanDanMukZic, this, null);
            return;
        }
    }

    public override void Die() {
        EventManager.Instance.PostNotification(BossStoneGolemEventType.Die, this, null);
    }

    public void OnEvent<TEventType>(TEventType eventType, Component sender, object param = null) where TEventType : Enum {
        if (eventType.Equals(BossStoneGolemEventType.Gigantic)) {
            // 거대화 
            _nextState = StateType.Gigantic;
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.DanDanMukZic)) {
            // 단단묵직
            _nextState = StateType.DanDanMukZic;
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.Die)) {
            // 죽음
            _nextState = StateType.Die;
            return;
        }
    }
}
