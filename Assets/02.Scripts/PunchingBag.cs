using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void Damage(float damage)
    {
        Debug.Log($"Damage: {damage}");
    }
}
