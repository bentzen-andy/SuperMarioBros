using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {
    public Transform connection;
    public KeyCode enterPipeKeyCode = KeyCode.DownArrow;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    private void OnTriggerStay2D(Collider2D other) {
        // don't allow player to enter the pipe if this pipe doesn't lead anywhere
        if (connection == null) return;
        Transform player = other.transform;
         
        if (other.CompareTag("Player")) {
            if (Input.GetKey(enterPipeKeyCode)) {
                StartCoroutine(Enter(player));
            }

        }
    }

    private IEnumerator Enter(Transform player) {
        // disable Mario's movement 
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection; 
        Vector3 enteredScale = Vector3.one / 2f;

        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        bool isUnderground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(isUnderground);

        // player is exiting through another pipe (as opposed to just spawning in a new location)
        if (exitDirection != Vector3.zero) {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        } else {
            // if player is exiting out into the open air
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
    }


    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale) {
        float elapsed = 0f;
        float duration = 1f;
        Vector3 startPosition = player.localPosition;
        Vector3 startScale = player.localScale;

        while (elapsed < duration) {
            float t = elapsed / duration;

            // this is linear interpolation
            player.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.localPosition = endPosition;
        player.localScale = endScale;
    }
}
