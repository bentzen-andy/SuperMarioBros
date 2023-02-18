using UnityEngine;

public class SideScrolling : MonoBehaviour {

    private Transform player;
    public float height = 8f;
    public float undergroundHeight = -7f;


    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }


    private void LateUpdate() {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(player.position.x, transform.position.x);
        transform.position = cameraPosition;
    }


    public void SetUnderground(bool isUnderground) {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = isUnderground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }

}
