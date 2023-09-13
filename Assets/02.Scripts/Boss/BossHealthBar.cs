using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider _bossHealthbarSlider;

    private void Awake()
    {
        _bossHealthbarSlider = GetComponentInChildren<Slider>();        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_bossHealthbarSlider != null)
        {
            // ü�¹ٸ� ������Ʈ�մϴ�.
            float healthRatio = currentHealth / maxHealth;
            _bossHealthbarSlider.value = healthRatio;
        }
    }
}
