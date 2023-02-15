using UnityEngine;

public class Goomba : MonoBehaviour {
    // todo add collision detection between mario and goomba
    // todo make goomba flat when mario jumps on him

    public Sprite flatSprite;
    // private new Rigidbody2D rigidbody;
    // private bool isStomped = false;


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (other.transform.DotProductTest(transform, Vector2.down)) {
                // player is moving down and landed on goomba
                FlattenGoomba();
            }
        }
    }


    private void FlattenGoomba() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 1f);
    }


}
