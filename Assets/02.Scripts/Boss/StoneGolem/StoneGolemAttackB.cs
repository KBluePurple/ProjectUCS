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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newStoneGolemProjectile.transform.rotation = targetRotation;


        newStoneGolemProjectile.Init(5f, _attackDamage, direction);
    }
}
