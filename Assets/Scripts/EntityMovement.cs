using UnityEngine;

public class EntityMovement : MonoBehaviour {
    private new Rigidbody2D rigidbody;

    public Vector2 velocity;
    public Vector2 direction = Vector2.left;
    public float moveSpeed = 2f;


    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }


    private void OnBecameVisible() {
        enabled = true;
    }


    private void OnBecameInvisible() {
        enabled = false;
    }


    private void OnEnable() {
        rigidbody.WakeUp();
        // velocity = new Vector2(-moveSpeed, 0);
    }


    private void OnDisable() {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }


    private void FixedUpdate() {
        Move();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        CheckForBarrier(collision);
    }


    private void CheckForBarrier(Collision2D collision) {
        bool entityHitABarrier = (Mathf.Abs(collision.contacts[0].normal.x)) > 0f;
        if (entityHitABarrier) direction.x *= -1f;
    }


    private void Move() {
        velocity.x = direction.x * moveSpeed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);


        if (rigidbody.Raycast(direction)) {
            direction = -direction;
        }

        if (rigidbody.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        if (direction.x > 0f) {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else if (direction.x < 0f) {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
