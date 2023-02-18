using UnityEngine;
using System.Collections;

public class BlockCoin : MonoBehaviour {
    
    private void Start() {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {
        Debug.Log("animate coin");
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f; 
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        Destroy(gameObject);
    }


    private IEnumerator Move(Vector3 from, Vector3 to) {
        // this is kind of like a "tweening" system
        float elapsed = 0f;
        float duration = 0.25f;
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
