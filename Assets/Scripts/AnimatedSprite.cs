using UnityEngine;

public class AnimatedSprite : MonoBehaviour {
    public Sprite[] sprites;
    public float fps = 10f;

    private SpriteRenderer spriteRenderer;
    private int frame = 0;


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable() {
        InvokeRepeating(nameof(Animate), fps, fps);
    }


    private void OnDisable() {
        CancelInvoke();
    }


    private void Animate() {
        frame++;
        if (frame >= sprites.Length) frame = 0;

        //  check if array index id out of bounds
        if (frame >= 0 && frame < sprites.Length) {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
