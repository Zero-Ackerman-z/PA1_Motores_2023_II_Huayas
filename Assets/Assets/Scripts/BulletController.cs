using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Colisión con un objeto que tiene el tag "Enemy"
            Destroy(gameObject);
        }
    }
}





