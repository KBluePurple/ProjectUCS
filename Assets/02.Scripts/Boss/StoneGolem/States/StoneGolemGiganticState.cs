using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemGiganticState : StoneGolemBaseState
{
    private CooldownTimer _phaseChangeTimer;
    private float _originY;

    public StoneGolemGiganticState(Entity entity) : base(entity) { }

    public override void OnStateEnter()
    {
        Debug.Log("Gigantic");

        _bossStoneGolem.Animator.SetTrigger("Gigantic");

        _phaseChangeTimer = new CooldownTimer(1.2f);
        _phaseChangeTimer.StartCooldown();

        _originY = _bossStoneGolem.transform.position.y;
    }

    public override void OnStateUpdate()
    {
        _phaseChangeTimer?.UpdateTimer();

        float scale = Mathf.Lerp(1f, 3f, _phaseChangeTimer.GetNormalizedTimer());
        Vector3 newScale = Vector3.one * scale;
        _bossStoneGolem.SetScale(newScale);

        float y = Mathf.Lerp(_originY, _originY + 3f, _phaseChangeTimer.GetNormalizedTimer());
        Vector3 newPosition = _bossStoneGolem.transform.position;
        newPosition.y = y;
        _bossStoneGolem.transform.position = newPosition;

        // TODO: Gigantic 부분 수정해야함
        if (_phaseChangeTimer.IsCooldownReady())
        {
            _bossStoneGolem.SetScale(Vector2.one * 3);
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}