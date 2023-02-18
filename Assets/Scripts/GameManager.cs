using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    public void Awake() {
        if (Instance != null) DestroyImmediate(gameObject);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }


    private void Start() {
        Application.targetFrameRate = 60;
        StartNewGame();
    }


    public void StartNewGame() {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }


    public void LoadLevel(int world, int stage) {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }


    public void LoadNextLevel() {
        if (stage < 3) LoadLevel(world, stage + 1);
        else if (world <= 2) LoadLevel(world + 1, 1);
        else GameOver();
    }


    public void ResetLevel(float delay) {
        Invoke(nameof(ResetLevel), delay);
    }


    public void ResetLevel() {
        lives--;
        if (lives > 0) LoadLevel(world, stage);
        else GameOver();
    }


    private void GameOver() {
        Debug.Log("Game Over");
        StartNewGame();
        // TODO load the game over scene
        // TODO revert to main menu
    }


    public void AddCoin() {
        coins++;
        if (coins == 100) {
            coins = 0;
            AddLife();
        }
    }


    public void AddLife() {
        lives ++;
    }
}
