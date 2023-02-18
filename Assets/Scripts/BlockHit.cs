using UnityEngine;
using System.Collections;

public class BlockHit : MonoBehaviour {
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1; // TODO see if I can change this to positive infinity 
    private bool isAnimating; 


    private void OnCollisionEnter2D(Collision2D other) {
        if (!isAnimating && maxHits != 0 && other.gameObject.CompareTag("Player")) {
            if (other.transform.DotProductTest(transform, Vector2.up)) {
                Hit();
            }
        }
    }


    private void Hit() {
        SpriteRenderer spriteRenderer =  GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;
        if (maxHits == 0) spriteRenderer.sprite = emptyBlock;
        if (item != null) {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        StartCoroutine(Animate()); 
    }


    private IEnumerator Animate() {
        isAnimating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up / 2f; 
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        isAnimating = false;
    }


    private IEnumerator Move(Vector3 from, Vector3 to) {
        // this is kind of like a "tweening" system
        float elapsed = 0f;
        float duration = 0.125f;
        while (elapsed < duration) {
            float t = elapsed / duration;
             // this is linear interpolation
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
