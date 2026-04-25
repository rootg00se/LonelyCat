using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteLibrary))]
public class SwapSkin : MonoBehaviour {
    [Header("Sprite Libraries")]
    [SerializeField] private SpriteLibraryAsset _defaultSpriteLibrary;
    [SerializeField] private SpriteLibraryAsset _electricSpriteLibrary;

    private SpriteLibrary _spriteLibrary;

    private void Awake() {
        _spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
    }

    public void SwapAsset() {
        if (PlayerElectricity.Instance.IsCharged) {
            _spriteLibrary.spriteLibraryAsset = _electricSpriteLibrary;
        } else {
            _spriteLibrary.spriteLibraryAsset = _defaultSpriteLibrary;
        }
    }

    public void AllowInteractions() {
        PlayerMovement.Instance.StartInteraction();
    }
}
