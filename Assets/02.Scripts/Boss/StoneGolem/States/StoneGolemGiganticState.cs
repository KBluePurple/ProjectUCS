using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemGiganticState : BaseState {
    private BossStoneGolem _bossStoneGolem;

    private CooldownTimer _phaseChangeTimer;

    public StoneGolemGiganticState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("Gigantic");

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