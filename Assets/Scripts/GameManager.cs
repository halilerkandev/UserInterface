using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;

    private float _spawnRate = 1.0f;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
        _score = 0;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(true)
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
}
