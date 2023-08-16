using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemBaseState : BaseState {
    protected BossStoneGolem _bossStoneGolem;

    public StoneGolemBaseState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() { }

    public override void OnStateUpdate() { }

    public override void OnStateExit() { }

}
