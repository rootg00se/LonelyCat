using UnityEngine;

public class SetParentOnCollision : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.contacts[0].normal.y < -0.5f) {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
    }
}
