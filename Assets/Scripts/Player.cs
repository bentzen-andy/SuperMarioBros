using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerSpriteRenderer spriteRendererBig;
    public PlayerSpriteRenderer spriteRendererSmall;
    public DeathAnimation deathAnimation;

    public bool isBig => spriteRendererBig.enabled;
    public bool isSmall => spriteRendererSmall.enabled;
    public bool isDead => deathAnimation.enabled;


    private void Awake() {
        deathAnimation = GetComponent<DeathAnimation>();
    }


    public void Hit() {
        if (isBig) {
            Shrink();
        }
        else if (isSmall) {
            Death();
        }
    }


    private void Shrink() {
        spriteRendererBig.enabled = false;
        spriteRendererSmall.enabled = true;
    }


    private void Death() {
        spriteRendererBig.enabled = false;
        spriteRendererSmall.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
            
        
    }
}
