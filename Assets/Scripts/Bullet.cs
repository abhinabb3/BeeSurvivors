using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 10;
    public int speed = 200;
    public int timer = 1;

    [Header("Components")]
    public GameObject hitFX;

    private void Start()
    {
        Destroy(gameObject, timer);
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Instantiate(hitFX, transform.position, transform.rotation); // explosion

            collision.transform.GetComponent<Health>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }

}


