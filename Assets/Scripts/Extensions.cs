using UnityEngine;

public static class Extensions {

    private static LayerMask layerMask = LayerMask.GetMask("Default");


    // returns true if the rigidbody is touching a collider in the direction
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) {
        if (rigidbody.isKinematic) return false;

        Collider2D collider = rigidbody.GetComponent<Collider2D>();
        var capsuleCollider = collider as CapsuleCollider2D;
        var circleCollider = collider as CircleCollider2D;

        float size = 0f;
        if (capsuleCollider != null) {
            if ((direction == Vector2.up) || (direction == Vector2.down)) {
                size = capsuleCollider.size.y / 4f;
            }
            else if ((direction == Vector2.left) || (direction == Vector2.right)) {
                size = capsuleCollider.size.x / 4f;
            }
        } else if (circleCollider != null) {
            size = circleCollider.radius / 2f;
        } else {
            Debug.LogError("Collider not supported");
            return false;
        }
        float radius = size;
        float distance = size + 0.05f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }


    // returns true if the rigidbody is touching a collider in the direction
    public static bool DotProductTest(this Transform transform, Transform other, Vector2 testDirection) {
        Vector2 direction = other.position - transform.position;
        float dotProduct = Vector2.Dot(direction.normalized, testDirection.normalized);
        // Debug.Log(other.gameObject + ":::::::::" + dotProduct);
        return dotProduct > 0.25f;
    }

}
