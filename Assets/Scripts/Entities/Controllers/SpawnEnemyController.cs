using System;
using UnityEditor;
using UnityEngine;

public enum EnemyType
{
    HyukM,
    SeWoong,
    YuRok
}

public class SpawnEnemyController : MonoBehaviour
{
    [SerializeField] private int spawnFrequency = 10; // 스폰 주기
    private float currTime = 0;

    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > spawnFrequency)
        {
            SpawnMonster();
            currTime = 0;
        }
    }

    private void SpawnMonster()
    {
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool("Enemy");
        obj.transform.position = new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), 3.7f);

        Array values = Enum.GetValues(typeof(EnemyType));
        EnemyType randomEnemyType = (EnemyType)values.GetValue(UnityEngine.Random.Range(0, values.Length));

        RuntimeAnimatorController animatorController;
        switch (randomEnemyType)
        {
            case EnemyType.SeWoong:
                animatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/Enemy/SeWoong/SeWoong.controller");
                obj.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;
                break;
            case EnemyType.YuRok:
                animatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/Enemy/YuRok/YuRok.controller");
                obj.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;
                break;
            case EnemyType.HyukM:
                animatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animations/Enemy/Hyuk/Hyuk.controller");
                obj.GetComponentInChildren<Animator>().runtimeAnimatorController = animatorController;
                break;
        }        
    }
}