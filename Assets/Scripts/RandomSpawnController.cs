using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class RandomSpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels = new List<GameObject>();
    
    void Start()
    {
        GameObject randomLevel = _levels[Random.Range(1, _levels.Count)];

        Instantiate(randomLevel, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
