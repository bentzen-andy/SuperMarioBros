using UnityEngine;

public class SideScrolling : MonoBehaviour {
    
    private Transform player;


    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }


    private void LateUpdate() {
        // transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(player.position.x, transform.position.x);
        transform.position = cameraPosition;
    }

}
