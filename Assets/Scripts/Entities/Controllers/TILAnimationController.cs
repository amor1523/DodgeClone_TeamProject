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
        // �����ϰų� ������ �� �ִϸ��̼��� ���� �����ϵ��� ����
        //controller.OnAttackEvent += Attacking;
        //controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    // �ǰ�
    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    // �ǰݴ��ϴ� ������ ���� ����
    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
