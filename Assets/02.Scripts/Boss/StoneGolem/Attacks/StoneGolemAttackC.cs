using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackC : MonoBehaviour {
    private BossStoneGolem _bossStoneGolem;

    private StoneGolemLaser _stoneGolemLaser;
    [SerializeField] private float _attackDamage;

    public void Init(BossStoneGolem bossStoneGolem) {
        _bossStoneGolem = bossStoneGolem;

        _stoneGolemLaser = GetComponentInChildren<StoneGolemLaser>();
        _stoneGolemLaser.Init(_attackDamage);
    }

    public void Attack(GameObject target) {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetRotation;

        Vector3 scale = _bossStoneGolem.transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;

        _stoneGolemLaser.Attack();
    }

    public void EndAttack() {
        _stoneGolemLaser.EndAttack();
    }
}
