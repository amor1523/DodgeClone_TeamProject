using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropHealthPotion : MonoBehaviour
{
    private HealthSystem enemyHealth;

    private void Awake()
    {
        this.enemyHealth = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        this.enemyHealth.OnDeath += CreateItem;
    }

    public void CreateItem()
    {
        GameObject potion = GameManager.Instance.ObjectPool.SpawnFromPool("HealthPotion");
        float x = Random.RandomRange(-2.5f, 2.5f);
        float y = Random.RandomRange(-3.5f, 3.5f);
        potion.transform.position = new Vector2(x, y);
    }
}
