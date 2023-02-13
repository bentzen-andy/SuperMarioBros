using UnityEngine;

public static class Extensions {

    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) {
        if (rigidbody.isKinematic) return false;
        
        float playerHeight = rigidbody.GetComponent<CapsuleCollider2D>().size.y;
        float radius = playerHeight/4f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
}
