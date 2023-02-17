using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    public float deathDuration = 3f;
    public float elapsedTime = 0f;

    
    private void Reset() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable() {
        DisablePhysics();
        UpdateSprite();
        StartCoroutine(Animate());
        
    }


    private void UpdateSprite() {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        if (deadSprite != null) spriteRenderer.sprite = deadSprite;
        Debug.Log(deadSprite);
    }


    private void DisablePhysics () {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        PlayerMovement pm = GetComponent<PlayerMovement>();
        EntityMovement em = GetComponent<EntityMovement>();
        if (em != null) em.enabled = false;
        if (pm != null) pm.enabled = false;
    }

    
    private IEnumerator Animate() {

        float jumpVelocity = 10f;
        float gravity = -36f;
        
        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsedTime < deathDuration) {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
