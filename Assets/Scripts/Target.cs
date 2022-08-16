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

    // Start is called before the first frame update
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();

        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown() => Destroy(gameObject);

    private void OnTriggerEnter(Collider other) => Destroy(gameObject);

    private Vector3 RandomForce() => Vector3.up * Random.Range(_minSpeed, _maxSpeed);

    private Vector3 RandomTorque() => new(Random.Range(-_maxTorque, _maxTorque),
                                          Random.Range(-_maxTorque, _maxTorque),
                                          Random.Range(-_maxTorque, _maxTorque));

    private Vector3 RandomSpawnPos() => new(Random.Range(-_xRange, _xRange), _ySpawnPos);
}
