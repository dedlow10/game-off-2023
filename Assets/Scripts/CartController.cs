using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CartController : MonoBehaviour
{
    [SerializeField] float MovementSpeed;

    private Vector3 movement;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MovementSpeed * GameManager.Instance.GetCartSpeedBoost() * Time.fixedDeltaTime);
    }
}
