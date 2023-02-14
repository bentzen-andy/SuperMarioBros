using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite run;
    public Sprite jump;
    public Sprite slide;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate() {
        if (movement.isJumping) {
            spriteRenderer.sprite = jump;
        } else if (movement.isSliding) {
            spriteRenderer.sprite = slide;
        } else if (movement.isRunning) {
            spriteRenderer.sprite = run;
        } else {
            spriteRenderer.sprite = idle;
        }
    }
}
