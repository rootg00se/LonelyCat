using UnityEngine;

public class ButtonBlock : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _defaultSprite;

    public void ChangeVisual(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void ResetVisual() {
        _spriteRenderer.sprite = _defaultSprite;
    }
}
