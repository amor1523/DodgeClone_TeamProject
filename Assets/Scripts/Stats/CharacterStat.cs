using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add �����ϰ�, Multiple�ϰ�, �������� Override�ϴ� ������
// enum���� ���� ������ ���εǾ��ֱ� ������ ���� (0, 1, 2,...) 
// => ���Ŀ� ���� Ȱ���ϸ� ������������ ����Ȱ���ؼ� ü�������� ����ȿ�� ������� ���� ����
public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override, // 2
}

// Ŭ������ Serializable�� ����θ� �����Ǿ� ������ [Serializable]�� �ٿ� �����Ϳ��� Ȯ�� ����
[Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}
