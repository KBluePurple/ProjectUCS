using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Entity {

    public override void Init() {
        base.Init();

        _healthSystem.Init(this, 100);
    }

    public override void Die() {
        Debug.Log("Á×À½");

        //BaseBoss boss = FindObjectOfType<BaseBoss>();
        //if (boss != null) {
        //    boss.RemovePlayerList(this);
        //}
    }
}
