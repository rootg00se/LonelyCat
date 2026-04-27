using System;
using UnityEngine;

public class ElectricBlock : ElectricitySource {
    public override string AnimationTrigger => "Shock";
    public event Action OnCharge;

    private bool _isUsed = false;
    private Rigidbody2D _rigidBody;

    private void Awake() {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (_isUsed) return;

        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.SetCurrentSource(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            playerElectricity.ResetCurrentSource();
        }
    }

    private void UnlockBlock() {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void Interact() {
        _isUsed = true;
        
        OnCharge?.Invoke();
        UnlockBlock();
    }
}
