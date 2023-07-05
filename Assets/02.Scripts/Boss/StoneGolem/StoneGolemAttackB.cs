using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackB : MonoBehaviour {
    private BossStoneGolem _bossStoneGolem;

    public StoneGolemProjectile _stoneGolemProjectile;

    [SerializeField] private float _attackDamage;

    public void Init(BossStoneGolem bossStoneGolem) {
        _bossStoneGolem = bossStoneGolem;
    }

    public void Attack(Entity target) {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        StoneGolemProjectile newStoneGolemProjectile = Instantiate(_stoneGolemProjectile, null);
        newStoneGolemProjectile.transform.position = transform.position;

        // Todo: È¸Àü
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;

        newStoneGolemProjectile.Init(5f, _attackDamage, direction);
    }
}
