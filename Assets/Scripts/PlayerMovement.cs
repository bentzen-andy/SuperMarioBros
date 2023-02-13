using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private float inputAxis;
    public float moveSpeed = 8f;


    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }   


    private void Update() {
        HorizontalMovement();
    }


    private void FixedUpdate() {
        // rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }


    private void HorizontalMovement() {
        float inputAxis = Input.GetAxisRaw("Horizontal");
        // Vector2 move = new Vector2(horizontal, 0f);
        // rigidbody.velocity = move * moveSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }
}
