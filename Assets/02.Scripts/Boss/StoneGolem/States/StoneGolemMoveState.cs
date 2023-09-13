using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemMoveState : StoneGolemBaseState {
    private float _distanceMoved = 0f;
    private float _moveSpeed;

    public StoneGolemMoveState(Entity entity) : base(entity) {
        _moveSpeed = _bossStoneGolem.BossStats.MoveSpeed;
    }

    public override void OnStateEnter() {
        //Debug.Log("Move");

        _bossStoneGolem.Animator.SetBool("isMove", true);
    }

    public override void OnStateUpdate() {
        if (_bossStoneGolem.Target != null) {
            Vector2 direction = new Vector2((_bossStoneGolem.Target.transform.position.x - _bossStoneGolem.transform.position.x), 0);

            if (Mathf.Abs(direction.x) >= _bossStoneGolem.AttackACondition.AttackRange / 2) {
                direction.Normalize();
                _bossStoneGolem.Move(direction);
                _distanceMoved += direction.x * _moveSpeed * Time.deltaTime;
            }
            else {
                _bossStoneGolem.Move(Vector2.zero);
            }
        }

        if (_distanceMoved >= 10f) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
            return;
        }

        // 이동 중에 공격할 수 잇으면 공격할지
        _bossStoneGolem.AttackACondition?.UpdateTimer();
        if (_bossStoneGolem.AttackACondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackA);
            return;
        }

        _bossStoneGolem.AttackBCondition?.UpdateTimer();
        if (_bossStoneGolem.AttackBCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackB);
            return;
        }

        _bossStoneGolem.AttackCCondition?.UpdateTimer();
        if (_bossStoneGolem.AttackCCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackC);
            return;
        }

        _bossStoneGolem.HealCondition?.UpdateTimer();
        if (_bossStoneGolem.HealCondition.CanSkill()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Heal);
            return;
        }
    }

    public override void OnStateExit() {
        _bossStoneGolem.Move(Vector2.zero);
    }
}
