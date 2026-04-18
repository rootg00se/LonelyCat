using UnityEngine;

public class SetParentOnCollision : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
    }
}
