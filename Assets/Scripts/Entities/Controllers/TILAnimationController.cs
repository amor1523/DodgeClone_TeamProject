using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TILAnimationController : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Animator animator;

    private static readonly int IsHit = Animator.StringToHash("isHit");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Start()
    {
        // 공격하거나 움직일 때 애니메이션이 같이 반응하도록 구독
        //controller.OnAttackEvent += Attacking;
        //controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    // 피격
    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    // 피격당하는 동안은 무적 상태
    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
