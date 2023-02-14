using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite idle;
    public AnimatedSprite run;
    public Sprite jump;
    public Sprite slide;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }


    private void OnEnable() {
        spriteRenderer.enabled = true;
    }


    private void OnDisable() {
        spriteRenderer.enabled = false;
    }


    private void LateUpdate() {
        run.enabled = movement.isRunning;

        if (movement.isJumping) {
            spriteRenderer.sprite = jump;
        } else if (movement.isSliding) {
            spriteRenderer.sprite = slide;
        } else if (!movement.isRunning) {
            spriteRenderer.sprite = idle;
        }
    }
}
