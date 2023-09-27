using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f; // Distancia del raycast
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool persiguiendo = false;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update()
    {
        if (DetectPlayer()) // Verifica si detecta al jugador
        {
            persiguiendo = true;
        }
        else if (persiguiendo) // Si estaba persiguiendo y ya no detecta al jugador
        {
            persiguiendo = false;
            myRBD2.velocity = Vector2.zero; // Detiene al enemigo
        }

        if (persiguiendo)
        {
            // Si est� persiguiendo al jugador, mu�vete hacia la posici�n del jugador o su �ltima ubicaci�n conocida.
            MoveTowardsPlayer();
        }
        else
        {
            // Si no est� persiguiendo, contin�a patrullando.
            Patrol();
        }

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        Debug.DrawRay(transform.position, transform.right * rayDistance, Color.red); // Dibuja el raycast hacia adelante.
    }

    private void Patrol()
    {
        CheckNewPoint();
    }

    private void MoveTowardsPlayer()
    {
        // Implementa aqu� la l�gica para mover al enemigo hacia el jugador o su �ltima ubicaci�n conocida.
        // Puedes usar myRBD2.velocity para establecer la velocidad en la direcci�n correcta.
    }

    private bool DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Si el raycast golpea al jugador, devuelve true (detectado).
            return true;
        }
        else
        {
            // Si no detecta al jugador, devuelve false.
            return false;
        }
    }

    private void CheckNewPoint()
    {
        if (!persiguiendo && Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length ? 0 : patrolPos + 1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
