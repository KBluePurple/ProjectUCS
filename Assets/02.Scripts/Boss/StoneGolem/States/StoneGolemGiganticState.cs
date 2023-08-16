using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemGiganticState : StoneGolemBaseState {
    private CooldownTimer _phaseChangeTimer;

    public StoneGolemGiganticState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("Gigantic");

        _bossStoneGolem.Animator.SetTrigger("Gigantic");

        _phaseChangeTimer = new CooldownTimer(1.2f);
        _phaseChangeTimer.StartCooldown();
    }

    public override void OnStateUpdate() {
        _phaseChangeTimer?.UpdateTimer();

        float scale = Mathf.Lerp(1f, 3f, _phaseChangeTimer.GetNormalizedTimer());
        _bossStoneGolem.SetScale(Vector3.one * scale);

        // TODO: Gigantic 부분 수정해야함
        if (_phaseChangeTimer.IsCooldownReady()) {
            _bossStoneGolem.SetScale(Vector2.one * 3);
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}