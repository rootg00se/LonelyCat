using UnityEngine;

public class MagneticForce : MonoBehaviour {
    [Header("Magnetic Force Settings")]
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _liftForce;
    [SerializeField] private float _damping;
    [SerializeField] private float _magneticObjectHeight;

    [Header("Bobbing Settings")]
    [SerializeField] private float _bobAmplitude;
    [SerializeField] private float _bobFrequency;

    [Header("Smooth Settings")]
    [SerializeField] private float _smoothTime = 0.2f;
    [SerializeField] private float _maxLiftSpeed = 10f;

    private float _velocityY;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.TryGetComponent(out PlayerMovement playerMove)) {
            if (PlayerElectricity.Instance.IsCharged) {
                Rigidbody2D rb = playerMove.GetComponent<Rigidbody2D>();
        
                float playerY = rb.position.y;
                float magnetY = transform.position.y;
                float liftOffThreshold = magnetY + _magneticObjectHeight; 

                bool isPlayerCharged = PlayerElectricity.Instance.IsCharged;

                if (isPlayerCharged && !playerMove.GetIsGrounded()) {
                    if (playerMove.GetIsGrounded() && playerY > liftOffThreshold) {
                        playerMove.SetLevitation(false);
                } else {
                    Levitate(playerMove);
                }
            }
        } else {
                playerMove.SetLevitation(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent(out PlayerMovement playerMove)) {
            playerMove.SetLevitation(false);
        }
    }

    private void Levitate(PlayerMovement player) {
        player.SetLevitation(true);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        float baseTargetY = transform.position.y + _maxHeight;
        float bobbingOffset = Mathf.Sin(Time.time * _bobFrequency) * _bobAmplitude;
        float finalTargetY = baseTargetY + bobbingOffset;

        float nextY = Mathf.SmoothDamp(rb.position.y, finalTargetY, ref _velocityY, _smoothTime, _maxLiftSpeed);
        float requiredVelocityY = (nextY - rb.position.y) / Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, requiredVelocityY);
    }
}
