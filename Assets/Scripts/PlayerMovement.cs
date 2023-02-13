using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private new Rigidbody2D rigidbody;
    private new Camera camera;

    public Vector2 velocity;
    public float playerWidth;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce =>  (2f*maxJumpHeight)/(maxJumpTime/2f);
    public float gravity => -(2f*maxJumpHeight)/(maxJumpTime*maxJumpTime/4f);

    public bool isGrounded {get; private set;}
    public bool isJumping {get; private set;}


    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        playerWidth = GetComponent<CapsuleCollider2D>().size.x;
        camera = Camera.main;
    }   


    private void Update() {
        HorizontalMovement();

        isGrounded = rigidbody.Raycast(Vector2.down);

        if (isGrounded) GroundedMovement();
        else MidAirMovement();

        ApplyGravity();
    }


    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + playerWidth/2, rightEdge.x - playerWidth/2);

        rigidbody.MovePosition(position);
    }


    private void HorizontalMovement() {
        float inputAxis = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }


    private void GroundedMovement() {
        velocity.y = Mathf.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Jumping");
            isJumping = true;
            velocity.y = jumpForce;
        }
    }


    private void MidAirMovement() { 
        if (Input.GetButtonUp("Jump")) {
            Debug.Log("Not Jumping");
            isJumping = false;
            if (velocity.y > 0) {
                velocity.y = Mathf.MoveTowards(velocity.y, 0f, gravity * Time.deltaTime);
            }
        }
    }


    private void ApplyGravity() {
        if (isGrounded) return;
        float multiplier = isJumping ? 1f : 2f;
        float gravityWithMultiplier = gravity * multiplier;
        velocity.y += gravityWithMultiplier * Time.deltaTime;}
}
