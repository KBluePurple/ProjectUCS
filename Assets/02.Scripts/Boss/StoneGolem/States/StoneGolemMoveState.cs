using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemMoveState : MoveState {
    private BossStoneGolem _bossStoneGolem;

    public CooldownTimer MovingCooldownTimer => _movingCooldownTimer;

    protected CooldownTimer _movingCooldownTimer;


    public StoneGolemMoveState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("Move");

        _movingCooldownTimer = new CooldownTimer(2f);
    }

    public override void OnStateUpdate() {
        if (_bossStoneGolem.Target != null) {
            Vector2 direction = new Vector2((_bossStoneGolem.Target.transform.position.x - _bossStoneGolem.transform.position.x), 0);

            if (Mathf.Abs(direction.x) >= 0.3f) {
                direction.Normalize();
                _bossStoneGolem.Move(direction);
            }
            else {
                _bossStoneGolem.Move(Vector2.zero);
            }
        }

        _movingCooldownTimer.UpdateTimer();

        if (_movingCooldownTimer.IsCooldownReady()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
            return;
        }

        // 이동 중에 공격할 수 잇으면 공격할지
        if (_bossStoneGolem.AttackACondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackA);
            return;
        }

        if (_bossStoneGolem.AttackBCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackB);
            return;
        }

        if (_bossStoneGolem.AttackCCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackC);
            return;
        }
    }

    public override void OnStateExit() {
        _bossStoneGolem.Move(Vector2.zero);
    }
}
