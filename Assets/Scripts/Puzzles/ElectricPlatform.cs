using UnityEngine;

public class ElectricPlatform : ElectricitySource {
    public override string AnimationTrigger => "PlatformShock";

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerElectricity playerElectricity)) {
            if (collision.contacts[0].normal.y < -0.5f) {
                playerElectricity.ExecuteInteraction(this);
            }
        }
    }

    public override void Interact() { }
}
