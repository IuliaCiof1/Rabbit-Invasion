using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] itemsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itemsPrefab.Length);
        int i = Random.Range(0, itemsPrefab.Length);
        GameObject newItem = Instantiate(itemsPrefab[i], transform);
    }
    
}
