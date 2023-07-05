using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats {
    public int Health { get; private set; }
    public int Mana { get; private set; }
    public int Defense { get; private set; }
    public int AttackPower { get; private set; }
    public int MovementSpeed { get; private set; }
    public float Concentration { get; private set; }

    public BossStats(int health, int mana, int defense, int attackPower, int movementSpeed, float concentration) {
        Health = health;
        Mana = mana;
        Defense = defense;
        AttackPower = attackPower;
        MovementSpeed = movementSpeed;
        Concentration = concentration;
    }
}
