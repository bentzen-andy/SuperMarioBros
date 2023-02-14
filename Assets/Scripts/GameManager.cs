using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }

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
        StartNewGame();
    }


    private void StartNewGame() {
        lives = 3;
        LoadLevel(1, 1);
    }


    private void LoadLevel(int world, int stage) {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }


    private void LoadNextLevel() {
        if (stage < 3) LoadLevel(world, stage + 1);
        else if (world <= 2) LoadLevel(world + 1, 1);
        else GameOver();
    }


    public void HandlePlayerDeath() {
        lives--;
        if (lives > 0) LoadLevel(world, stage);
        else GameOver();
    }


    private void GameOver() {
        Debug.Log("Game Over");
        // load the game over scene
        // revert to main menu
        Invoke(nameof(StartNewGame), 3f);
    }
}
