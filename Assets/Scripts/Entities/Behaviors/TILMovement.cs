using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TILMovement : MonoBehaviour
{
    private TILController controller;
    private Rigidbody2D movementRigidBody;
    private CharacterStatHandler characterStatHandler;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<TILController>();
        movementRigidBody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction; 
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed;
        movementRigidBody.velocity = direction;
    }
}
