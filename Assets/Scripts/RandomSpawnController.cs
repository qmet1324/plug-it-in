using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class RandomSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_prefab, transform.position + new Vector3((Random.Range(-5.0f, 5.0f)), 
                                                                                 0.0f, 
                                                                                 0.0f), 
                                                                                 transform.rotation);
        Destroy(_prefab);
    }
}
