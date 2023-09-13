using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider _bossHealthbarSlider;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_bossHealthbarSlider == null)
        {
            _bossHealthbarSlider = GetComponentInChildren<Slider>();
        }
        if (_bossHealthbarSlider != null)
        {
            // ü�¹ٸ� ������Ʈ�մϴ�.
            float healthRatio = currentHealth / maxHealth;
            _bossHealthbarSlider.value = healthRatio;
        }
    }
}