using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float _spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
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
}
