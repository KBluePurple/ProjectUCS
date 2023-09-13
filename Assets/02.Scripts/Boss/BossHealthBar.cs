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
            // 체력바를 업데이트합니다.
            float healthRatio = currentHealth / maxHealth;
            _bossHealthbarSlider.value = healthRatio;
        }
    }
}
