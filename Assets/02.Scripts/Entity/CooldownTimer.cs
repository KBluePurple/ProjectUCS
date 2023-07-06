using UnityEngine;

public class CooldownTimer {

    protected float _cooldownDuration;
    protected float _timer;

    private bool _isCooldownStarted;


    public CooldownTimer(float cooldownDuration) {
        _cooldownDuration = cooldownDuration;
    }

    public void StartCooldown() => _timer = 0f;

    public bool IsCooldownReady() => _timer >= _cooldownDuration;

    public void UpdateTimer() {
        if (IsCooldownReady())
            return;

        _timer += Time.deltaTime;
    }

    public void Delay(float delay = 1f) {
        _timer -= delay;
    }

    public float GetNormalizedTimer() => _timer / _cooldownDuration;
}
