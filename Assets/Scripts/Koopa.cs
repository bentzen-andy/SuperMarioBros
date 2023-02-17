using UnityEngine;

public class Koopa : MonoBehaviour {
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!shelled && collision.gameObject.CompareTag("Player")) {
            Debug.Log("jumped on koopa's head");

            if (collision.transform.DotProductTest(transform, Vector2.down)) {
                EnterShell();
            } else {
                Player player = collision.gameObject.GetComponent<Player>();
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (shelled && collision.CompareTag("Player")) {
            Debug.Log("touched a shelled koopa");
            if (!pushed) {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            } else {
                Player player = collision.gameObject.GetComponent<Player>();
                player.Hit();
            }
        } else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }

        
    }

    private void EnterShell() {
        Debug.Log("koopa hides in his shell");
        shelled = true;
        // GetComponent<Collider2D>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = shellSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        // Destroy(gameObject, 0.5f);
    }

    private void PushShell(Vector2 direction) {
        pushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.moveSpeed = shellSpeed;
        movement.enabled = true;
        Debug.Log("flag-PushShell()");
        Debug.Log(LayerMask.LayerToName(gameObject.layer));
        gameObject.layer = LayerMask.NameToLayer("Shell");
        Debug.Log(LayerMask.LayerToName(gameObject.layer));
    }

    private void Hit() {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    // private void OnBecameInvisible() {
    //     if (pushed) {
    //         Destroy(gameObject);
    //     }    
    // }

}