using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private float moveSpeed = 5f;
    private Transform player;
    private bool playerInRange = false;

    private void Start()
    {
        initialPosition = transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerInRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    private void ReturnToInitialPosition()
    {
        Vector3 direction = (initialPosition.position - transform.position).normalized;

        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = newPosition;

        if (Vector3.Distance(transform.position, initialPosition.position) < 0.1f)
        {
            transform.position = initialPosition.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
