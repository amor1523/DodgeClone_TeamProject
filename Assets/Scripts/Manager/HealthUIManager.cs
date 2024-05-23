using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private HealthSystem characterHealth;
    [SerializeField] private GameObject healthtPrefab; // 체력 UI 프리팹
    [SerializeField] private Transform healthsParent; // 체력 UI의 부모 객체

    private List<GameObject> healths = new List<GameObject>(); // 체력 UI를 관리할 리스트

    private int lastIndex;

    private void Start()
    {
        // 피해와 회복 이벤트에 대한 리스너 등록
        characterHealth.OnDamage += OnDamageTaken;
        characterHealth.OnHeal += OnHealReceived;

        CreateUI();
    }

    private void OnDestroy()
    {
        // 리스너 해제
        characterHealth.OnDamage -= OnDamageTaken;
        characterHealth.OnHeal -= OnHealReceived;
    }

    // 피해 이벤트 핸들러
    private void OnDamageTaken()
    {
        // 피해를 입으면 가장 오른쪽에 있는 체력UI 비활성화
        if(lastIndex > 0)
        {
            UpdateUI(lastIndex, false);
            lastIndex--;
        }
    }

    // 회복 이벤트 핸들러
    private void OnHealReceived()
    {
        // 회복을 받으면 가장 오른쪽에 비활성화된 체력UI 활성화
        if (lastIndex < healths.Count - 1)
        {
            lastIndex++;
            UpdateUI(lastIndex, true);
        }
    }

    private void UpdateUI(int index, bool isActive)
    {
        GameObject health = healths[index];
        health.SetActive(isActive);
    }

    // 플레이어의 최대 체력에 따라 UI를 생성 및 현재 체력만큼 활성화
    private void CreateUI()
    {
        for (int i = 0; i < characterHealth.MaxHealth; i++)
        {
            GameObject health = Instantiate(healthtPrefab, healthsParent);
            healths.Add(health);
            healths[i].SetActive(true);
        }
        lastIndex = characterHealth.MaxHealth - 1;
    }
}