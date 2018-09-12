using UnityEngine;

public class Cooldown {
    public float CooldownTime;
    private float _counter;
    private bool _hasStarted;
    
    public Cooldown(float cooldown) {
        CooldownTime = cooldown;
    }

    public bool IsOutCooldown() {
        if (!_hasStarted) {
            return true;
        }
        
        _counter -= Time.deltaTime;
        if (_counter < 0) {
            _hasStarted = false;
            return true;
        }

        return false;
    }

    public void Use() {
        _hasStarted = true;
        _counter = CooldownTime;
    }
    
}
