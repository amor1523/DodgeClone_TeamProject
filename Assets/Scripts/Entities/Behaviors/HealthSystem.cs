using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 1.0f;

    private CharacterStatHandler statsHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    // ü���� ������ �� �� �ൿ���� �����ϰ� ���� ����
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public int CurrentHealth { get; private set; }

    // ���ٽ� => get�� ������ ��ó�� ������Ƽ�� ����ϴ� ��, �������� �������� �������� ���ƴٴϴٰ� ��ũ�� ������ ������ ���� �� �ִ�
    public int MaxHealth => statsHandler.CurrentStat.maxHealth; // get { return statsHandler.CurrentStat.maxHealth; }

    private void Awake()
    {
        statsHandler = GetComponent<CharacterStatHandler>();
        
    }

    private void Start()
    {
        CurrentHealth = statsHandler.CurrentStat.maxHealth;
    }

    private void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(int change)
    {
        if (CurrentHealth <= 0f) // ���� �׾����� Ȯ��
        {
            CallDeath();
            CurrentHealth = statsHandler.CurrentStat.maxHealth;

            return true;
        }

        if (change >= 0) // + �� ���ִ� �ۿ��� �Ͼ�� OnHeal
        {
            OnHeal?.Invoke();
        }
        else // ���������� - �� ���ָ� OnDamage
        {
            // ���� �ð����� ü���� ���� ����
            if (isAttacked)
            {
                return false;
            }
            OnDamage?.Invoke();
            isAttacked = true;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        // [�ּڰ��� 0, �ִ��� MaxHealth�� �ϴ� ����]
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        // �ٸ�ǥ��
        // 1) CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        // 2) CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}