using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _valueImage = null;

    private HealthSystem _healthSystem = null;

    public void Init(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;

        StartCoroutine(HealthBarUIUpdateCoroutine());
    }

    private IEnumerator HealthBarUIUpdateCoroutine()
    {
        while (true)
        {
            _valueImage.fillAmount = _healthSystem.GetNormalizedHealth();
            yield return null;
        }
    }
}
