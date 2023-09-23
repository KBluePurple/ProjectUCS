using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackA : MonoBehaviour
{
    private BossStoneGolem _bossStoneGolem;
    private Collider2D _collider;
    private Effect _effect;

    private List<CharacterBase> _characterList = new List<CharacterBase>();

    [SerializeField] private float _attackDamage;

    public void Init(BossStoneGolem bossStoneGolem)
    {
        _bossStoneGolem = bossStoneGolem;
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;

        _effect = transform.parent.GetComponentInChildren<Effect>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player�� ���� �ʿ�
        //Test player = other.GetComponent<Test>();
        //if (player != null)
        //{
        //    _playerList.Add(player);
        //}

        CharacterBase character = other.GetComponentInParent<CharacterBase>();
        if (character != null)
        {
            _characterList.Add(character);
        }

    }

    public void Attack()
    {
        _characterList.Clear();
        _collider.enabled = true;
    }

    public void EndAttack()
    {
        _collider.enabled = false;
        Effect();

        foreach (CharacterBase character in _characterList)
        {
            // ����
            character.Player.HealthSystem.TakeDamage(_attackDamage);
            // Todo: �˹�ȿ�� �ʿ�

        }

        _characterList.Clear();
    }

    public void Effect()
    {
        _effect.Init();
    }

}
