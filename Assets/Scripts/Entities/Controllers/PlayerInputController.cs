using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TILController
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}