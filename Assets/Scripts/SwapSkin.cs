using UnityEngine;
using UnityEngine.U2D.Animation;

public class SwapSkin : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset _defaultSpriteLibrary;
    [SerializeField] private SpriteLibraryAsset _electricSpriteLibrary;

    private SpriteLibrary _spriteLibrary;

    private void Awake() {
        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    public void ChangeToElectricAsset() {
        _spriteLibrary.spriteLibraryAsset = _electricSpriteLibrary;
    }

    public void ChangeToDefaultAsset() {
        _spriteLibrary.spriteLibraryAsset = _defaultSpriteLibrary;
    }

    public void AllowInteractions() {
        PlayerMovement.Instance.StartInteraction();
    }
}
