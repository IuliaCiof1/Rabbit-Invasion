using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float enemyHealth;

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log($"{gameObject.name} - {enemyHealth} hp");
        if (enemyHealth <= 0)
            Destroy(gameObject);
    }
}
