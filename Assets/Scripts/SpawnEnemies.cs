using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] int enemiesToBeSpawned;
    [SerializeField] GameObject enemyPrefab;

    float interval = 1.5f;
    int enemiesSpawned;

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag.Equals("Player"))
        {
     

            while (enemiesSpawned < enemiesToBeSpawned)
            {
                Debug.Log(enemiesSpawned);
                enemiesSpawned++;
                StartCoroutine(SpawnEnemy(interval, enemyPrefab));
            }

           Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        //yield return new WaitForSeconds(interval);

        Debug.Log(transform.root.name);
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = transform.position+ new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        //newEnemy.GetComponent<>
        Debug.Log(newEnemy.transform.position);
        yield return new WaitForSeconds(interval);
        //newEnemy.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));

    }
}
