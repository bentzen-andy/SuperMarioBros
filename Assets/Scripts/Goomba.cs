using UnityEngine;

public class Goomba : MonoBehaviour {

    public Sprite flatSprite;


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null && player.starPower) Hit();
            if (player != null && player.starPower) return; 

            if (other.transform.DotProductTest(transform, Vector2.down)) {
                // player is moving down and landed on goomba
                FlattenGoomba();
            } else {
                // player got hit by goomba
                player.Hit();
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }


    private void Hit() {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }


    private void FlattenGoomba() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 2f);
    }


}
