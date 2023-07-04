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
        Die
    }

    public CooldownTimer MoveCooldownTimer => _moveCooldownTimer;
    public StoneGolemAttackACondition AttackACondition => _attackACondition;
    public StoneGolemAttackBCondition AttackBCondition => _attackBCondition;
    public StoneGolemAttackCCondition AttackCCondition => _attackCCondition;


    protected CooldownTimer _moveCooldownTimer;
    protected StoneGolemAttackACondition _attackACondition;
    protected StoneGolemAttackBCondition _attackBCondition;
    protected StoneGolemAttackCCondition _attackCCondition;


    protected StateType _curState;
    protected StateType _nextState;

    private void Start() {
        _curState = _nextState = StateType.Idle;
        _phaseIndex = 1;

        _fsm = new Fsm(new StoneGolemIdleState(this));

        EventManager.Instance.AddListener(BossStoneGolemEventType.Gigantic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.DanDanMukZic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.Die, this);

        _moveCooldownTimer = new CooldownTimer(4f);

        _attackACondition = new StoneGolemAttackACondition(this, 3f);
        _attackBCondition = new StoneGolemAttackBCondition(this, 8f);
        _attackCCondition = new StoneGolemAttackCCondition(this, 13f);
    }

    private void Update() {
        if (_curState == StateType.Idle) {
            _moveCooldownTimer?.UpdateTimer();
        }

        _attackACondition?.UpdateTimer();
        _attackBCondition?.UpdateTimer();
        _attackCCondition?.UpdateTimer();

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
                    _attackACondition.StartCooldown();
                    _attackBCondition.Delay();
                    _attackCCondition.Delay();
                    break;
                case StateType.AttackB:
                    _fsm.ChangeState(new StoneGolemAttackBState(this));
                    _attackACondition.Delay();
                    _attackBCondition.StartCooldown();
                    _attackCCondition.Delay();
                    break;
                case StateType.AttackC:
                    _fsm.ChangeState(new StoneGolemAttackCState(this));
                    _attackACondition.Delay();
                    _attackBCondition.Delay();
                    _attackCCondition.StartCooldown();
                    break;
                case StateType.Heal:
                    _fsm.ChangeState(new StoneGolemHealState(this));
                    break;
                case StateType.Die:
                    _fsm.ChangeState(new StoneGolemDieState(this));
                    break;
            }
        }

        _fsm?.UpdateState();
    }

    private void LateUpdate() {
        FindClosestPlayer();
        CheckTransitionToNextFhase();
    }
    public void SetNextState(StateType state) {
        _nextState = state;
    }

    private void CheckTransitionToNextFhase() {
        float value = _healthSystem.GetNormalizedHealth();
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
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.DanDanMukZic)) {
            // 단단묵직
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.Die)) {
            // 죽음
            return;
        }
    }
}
