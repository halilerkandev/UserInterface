using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _targetRb;
    private float _minSpeed = 12.0f;
    private float _maxSpeed = 16.0f;
    private float _maxTorque = 10.0f;
    private float _xRange = 4.0f;
    private float _ySpawnPos = -2.0f;

    private GameManager _gameManager;

    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();

        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (_gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, gameObject.transform.rotation);
            _gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (gameObject.CompareTag("Good") && _gameManager.lives > 0)
        {
            _gameManager.DecreaseLives();
        }

        if (_gameManager.lives == 0)
        {
            _gameManager.GameOver();
        }
    }

    private Vector3 RandomForce() => Vector3.up * Random.Range(_minSpeed, _maxSpeed);

    private Vector3 RandomTorque() => new(Random.Range(-_maxTorque, _maxTorque),
                                          Random.Range(-_maxTorque, _maxTorque),
                                          Random.Range(-_maxTorque, _maxTorque));

    private Vector3 RandomSpawnPos() => new(Random.Range(-_xRange, _xRange), _ySpawnPos);
}
