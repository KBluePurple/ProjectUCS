using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        AttackA, // ���� ����
        AttackB, // ���Ÿ� ����
        AttackC, // ������ ����
        Heal, // ��� �¼�
        Die
    }

    protected StateType _curState;
    protected StateType _nextState;

    private void Start() {
        _curState = _nextState = StateType.Idle;
        _phaseIndex = 1;

        _fsm = new Fsm(new StoneGolemIdleState(this));

        EventManager.Instance.AddListener(BossStoneGolemEventType.Gigantic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.DanDanMukZic, this);
        EventManager.Instance.AddListener(BossStoneGolemEventType.Die, this);
    }

    private void Update() {
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
            }
        }

        _fsm?.UpdateState();
    }

    private void LateUpdate() => CheckTransitionToNextFhase();

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
            // �Ŵ�ȭ 
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.DanDanMukZic)) {
            // �ܴܹ���
            return;
        }

        if (eventType.Equals(BossStoneGolemEventType.Die)) {
            // ����
            return;
        }
    }
}
