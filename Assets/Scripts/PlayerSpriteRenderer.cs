using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour {
    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }


    private void OnEnable() {
        spriteRenderer.enabled = true;
    }


    private void OnDisable() {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }


    private void LateUpdate() {
        if (!movement.isGrounded) spriteRenderer.sprite = jump;
        else if (movement.isSliding) spriteRenderer.sprite = slide;
        else if (movement.isRunning) run.enabled = true;
        else if (!movement.isRunning) spriteRenderer.sprite = idle;
    }
}
