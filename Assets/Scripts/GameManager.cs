using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;

    public GameObject titleScreen;

    public Button restartButton;

    [Min(0)] public int lives;

    private float _spawnRate = 1.0f;
    private int _score;

    private AudioSource _backgroundMusic;

    public Slider volumeSlider;

    public GameObject pausedImage;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _backgroundMusic = GetComponent<AudioSource>();
        UpdateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            TogglePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(targets[Random.Range(0, targets.Count)]);
        }
    }

    public void UpdateScore(int addToScore)
    {
        _score += addToScore;
        scoreText.text = "Score: " + _score;
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void UpdateVolume()
    {
        _backgroundMusic.volume = volumeSlider.value;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        _score = 0;
        _spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives();

        titleScreen.gameObject.SetActive(false);
    }

    public void DecreaseLives()
    {
        if (lives > 0)
        {
            lives -= 1;
            UpdateLives();
        }
    }

    void TogglePaused()
    {
        isPaused = !isPaused;
        pausedImage.SetActive(isPaused);
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
