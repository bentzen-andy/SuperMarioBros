using UnityEngine;

public static class Extensions {

    private static LayerMask layerMask = LayerMask.GetMask("Default");


    // returns true if the rigidbody is touching a collider in the direction
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) {
        if (rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

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
