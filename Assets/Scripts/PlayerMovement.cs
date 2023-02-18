using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private new Rigidbody2D rigidbody;
    private new Camera camera;

    public Vector2 velocity;
    public float playerWidth;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    // derived values
    public float directionX;
    public float jumpForce =>  (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => -(2f * maxJumpHeight) / (maxJumpTime * maxJumpTime / 4f);

    public bool isGrounded {get; private set;}
    public bool isJumping {get; private set;}
    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(directionX) > 0.25f;
    public bool isSliding => directionX > 0 && velocity.x < 0 || directionX < 0 && velocity.x > 0;


    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        playerWidth = GetComponent<CapsuleCollider2D>().size.x;
        camera = Camera.main;
    }   


    private void Update() {
        directionX = Input.GetAxisRaw("Horizontal");
        HorizontalMovement();

        isGrounded = rigidbody.Raycast(Vector2.down);
        if (isGrounded) GroundedMovement();
        else MidAirMovement();

        ApplyVerticalBarriers();
        ApplyGravity();
    }


    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        // get camera edges in world space
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // clamp player position to camera edges
        position.x = Mathf.Clamp(position.x, leftEdge.x + playerWidth/2, rightEdge.x - playerWidth/2);

        rigidbody.MovePosition(position);
    }


    // stops player from jumping if they hit their head
    void OnCollisionEnter2D(Collision2D other) {
        CheckIfPlayerBumpedHead(other);
        CheckIfPlayerJumpedOnEnemy(other);
    }


    // allows player to move left and right
    private void HorizontalMovement() {
        

        if (isSliding) {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, moveSpeed * Time.deltaTime);
        }

        HandleSpriteDirectionChange();

        velocity.x = Mathf.MoveTowards(velocity.x, directionX * moveSpeed, moveSpeed * Time.deltaTime);
    }


    // allows player to jump
    private void GroundedMovement() {
        // reset velocity
        velocity.y = Mathf.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump")) {
            // Debug.Log("Jumping");
            isJumping = true;
            velocity.y = jumpForce;
        }
    }


    // allows player to jump higher by holding jump button
    private void MidAirMovement() { 
        if (Input.GetButtonUp("Jump")) {
            // Debug.Log("Not Jumping");
            isJumping = false;
            if (velocity.y > 0) {
                velocity.y = Mathf.MoveTowards(velocity.y, 0f, gravity * Time.deltaTime);
            }
        }
    }


    // stops player if they hit a wall
    private void ApplyVerticalBarriers() {
        if (rigidbody.Raycast(Vector2.right)) {
            velocity.x = Mathf.Min(velocity.x, 0f);
        }
        if (rigidbody.Raycast(Vector2.left)) {
            velocity.x = Mathf.Max(velocity.x, 0f);
        }
    }


    // applies gravity to player
    private void ApplyGravity() {
        if (isGrounded) return;
        float multiplier = isJumping ? 1f : 2f;
        float gravityWithMultiplier = gravity * multiplier;
        velocity.y += gravityWithMultiplier * Time.deltaTime;
    }


    // stops player from jumping if they hit their head
    private void CheckIfPlayerBumpedHead(Collision2D other) {
        // Mario can't bump his head on power ups
        if (other.gameObject.layer == LayerMask.NameToLayer("PowerUp")) return;

        // if player is moving down and hits something above him
        // bool playerBumpedHeadDuringJump = (other.contacts[0].normal.y) < 0f;
        bool playerBumpedHeadDuringJump = transform.DotProductTest(other.transform, Vector2.up);

        if (playerBumpedHeadDuringJump) {
            isJumping = false;
            velocity.y = 0f;
        }
    }
 
    
    private void CheckIfPlayerJumpedOnEnemy(Collision2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        Player player = gameObject.GetComponent<Player>();
        bool playerLandedOnEnemy = transform.DotProductTest(other.transform, Vector2.down);
        if (playerLandedOnEnemy) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                if (!player.starPower) velocity.y = jumpForce;
            }
        }
    }


    // flips sprite depending on direction player is moving
    private void HandleSpriteDirectionChange() {
        if (velocity.x > 0) transform.eulerAngles = Vector3.zero;
        if (velocity.x < 0) transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

}
