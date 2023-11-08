using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float health;
    float maxHealth;
    [SerializeField] float speed = 5f;
    [SerializeField] float offset;
    [SerializeField] Animator animator;
    Vector3 move;

    [SerializeField] Slider healthBar;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + offset, transform.rotation.z);

        EnemyAI.Attack += TakeDamage;
        maxHealth = health;
    }
    
    void FixedUpdate()
    {

        Vector3 mousePosition = Input.mousePosition; //mouse position in Screen Space

        //1. By default, the cursor/mouse doesn't have depth (value on the z axis). If we don't specify the depth, 
        //the rotation of the player will be around the camera not the cursor.
        //2. Since this is a 3D game with top-down view, the depth (z axis) of the cursor in Screen space is
        // equivalent to y axis of World Space.

        mousePosition.z = Camera.main.transform.position.y;

        // Convert the mouse position to a world point
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // Calculate the direction from the player to the mouse position.
        // Points are represented in Unity using vectors and when you substract 2 vectors you get the direction to
        //which it points to to the other point
        Vector3 direction = mouseWorldPosition - transform.position;

        //rotate only when the cursor is not directly on character
        if ((System.Math.Round(direction.x,3) <= -0.018 || System.Math.Round(direction.x, 3) >= 0.018) &&
            (System.Math.Round(direction.z, 3) <= -0.018 || System.Math.Round(direction.z, 3) >= 0.018)
            )
        {
            //Calculate the angle that the character needs to rotate
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Apply the rotation to the player object
            transform.rotation = Quaternion.Euler(0, angle, 0);
            
        }

        //Debug.Log(Input.GetAxis(rb.position + move * speed * Time.fixedDeltaTime));

        animator.SetInteger("Horizontal", (int)Mathf.Ceil(Input.GetAxis("Horizontal")));
        animator.SetInteger("Vertical", (int)Mathf.Ceil(Input.GetAxis("Vertical")));

        //Vector3 move = Input.GetAxis("Vertical") * rb.transform.forward + Input.GetAxis("Horizontal") * rb.transform.right;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

        

    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;

        healthBar.value = health * 1 / maxHealth;

    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("dead");
            animator.SetInteger("Horizontal", 0);
            animator.SetInteger("Vertical", 0);
            animator.SetBool("Death", true);
            //gameObject.SetActive(false);
            this.enabled = false;

            StartCoroutine(Wait());
            
        }

        healthBar.value = health * 1 / maxHealth;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);

    }
}
