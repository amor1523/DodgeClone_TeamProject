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

    // 체력이 변했을 때 할 행동들을 정의하고 적용 가능
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public int CurrentHealth { get; private set; }

    // 람다식 => get만 구현된 것처럼 프로퍼티를 사용하는 것, 데이터의 복제본이 여기저기 돌아다니다가 싱크가 깨지는 문제를 막을 수 있다
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
        if (CurrentHealth <= 0f) // 먼저 죽었는지 확인
        {
            CallDeath();
            CurrentHealth = statsHandler.CurrentStat.maxHealth;

            return true;
        }

        if (change >= 0) // + 를 해주는 작용이 일어나면 OnHeal
        {
            OnHeal?.Invoke();
        }
        else // 최종적으로 - 를 해주면 OnDamage
        {
            // 무적 시간에는 체력이 달지 않음
            if (isAttacked)
            {
                return false;
            }
            OnDamage?.Invoke();
            isAttacked = true;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        // [최솟값을 0, 최댓값을 MaxHealth로 하는 구문]
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        // 다른표현
        // 1) CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        // 2) CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}