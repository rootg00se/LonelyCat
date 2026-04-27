using UnityEngine;

public class Button : MonoBehaviour {
    [Header("Base settings")]
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite _triggerBlockVisual; 
    [SerializeField] private Sprite _activeButtonVisual; 
    [SerializeField] private Sprite _inactiveButtonVisual;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out ButtonBlock buttonBlock)) {
            _obstacle.Hide();

            buttonBlock.ChangeVisual(_triggerBlockVisual);
            SetActiveVisual(true);
        } else if (collision.gameObject.TryGetComponent(out ElectricBlock _)) {
            _obstacle.Hide();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out ButtonBlock buttonBlock)) {
            _obstacle.Show();

            buttonBlock.ResetVisual();
            SetActiveVisual(false);
        } else if (collision.gameObject.TryGetComponent(out ElectricBlock _)) {
            _obstacle.Show();
        }
    }

    private void SetActiveVisual(bool active) {
        _spriteRenderer.sprite = active ? _activeButtonVisual : _inactiveButtonVisual;
    }
}
