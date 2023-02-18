using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public PlayerSpriteRenderer spriteRendererBig;
    public PlayerSpriteRenderer spriteRendererSmall;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool isBig => spriteRendererBig.enabled;
    public bool isSmall => spriteRendererSmall.enabled;
    public bool isDead => deathAnimation.enabled;
    public bool starPower { get; private set; }


    private void Awake() {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = spriteRendererSmall;
    }


    public void Hit() {
        if (starPower || isDead) return;

        if (isBig) {
            Shrink();
        }
        else if (isSmall) {
            Death(); 
        }
    }


    private void Death() {
        spriteRendererBig.enabled = false;
        spriteRendererSmall.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
            
        
    }


    public void Grow() {
        spriteRendererBig.enabled = true;
        spriteRendererSmall.enabled = false;
        activeRenderer = spriteRendererBig;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }


    private void Shrink() {
        spriteRendererBig.enabled = false;
        spriteRendererSmall.enabled = true;
        activeRenderer = spriteRendererSmall;

        capsuleCollider.size = new Vector2(1f, 0.5f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }


    private IEnumerator ScaleAnimation() {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 3 == 0) {
                spriteRendererSmall.enabled = !spriteRendererSmall.enabled;
                // spriteRendererBig.enabled = !spriteRendererBig.enabled;
                spriteRendererBig.enabled = !spriteRendererSmall.enabled; // todo check this
            }

            yield return null;
        }

        spriteRendererSmall.enabled = false;
        spriteRendererBig.enabled = false;

        activeRenderer.enabled = true;
    }


    public void StarPower(float duration = 10f) {
        StartCoroutine(StarPowerAnimation(duration));
    }


    private IEnumerator StarPowerAnimation(float duration) {
        starPower = true;

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;

        }

        spriteRendererBig.spriteRenderer.color = Color.white;
        spriteRendererSmall.spriteRenderer.color = Color.white;
        starPower = false;
    }

}
