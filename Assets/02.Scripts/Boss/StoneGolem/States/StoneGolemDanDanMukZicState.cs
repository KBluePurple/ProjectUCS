using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemDanDanMukZicState : StoneGolemBaseState {
    private CooldownTimer _phaseChangeTimer;

    public StoneGolemDanDanMukZicState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("DanDanMukZic");

        _phaseChangeTimer = new CooldownTimer(1f);
        _phaseChangeTimer.StartCooldown();
    }

    public override void OnStateUpdate() {
        _phaseChangeTimer?.UpdateTimer();

        if (_phaseChangeTimer.IsCooldownReady()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}