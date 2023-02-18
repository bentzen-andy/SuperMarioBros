using UnityEngine;

public class PowerUp : MonoBehaviour {
    public enum PowerUpType {
        Coin,
        ExtraLife, 
        MagicMushroom, 
        StarPower
    }

    public PowerUpType powerUpType; 

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Collect(other.gameObject);
        }
        
    }


    private void Collect(GameObject player) {
        switch (powerUpType) {
            case PowerUpType.Coin: 
                GameManager.Instance.AddCoin();
                break;
            case PowerUpType.ExtraLife: 
                GameManager.Instance.AddLife();
                break;
            case PowerUpType.MagicMushroom: 
                player.GetComponent<Player>().Grow();
                break;
            case PowerUpType.StarPower: 
                // todo
                break;
        }
        Destroy(gameObject);
    }
}
