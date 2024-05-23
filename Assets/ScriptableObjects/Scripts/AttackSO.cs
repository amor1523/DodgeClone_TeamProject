using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float count;
    public float duration;
    public float power;
    public float speed;
    public LayerMask target;
    public string bulletNameTag;
    public int numberofBulletshot;
    public float multupleBulletAngel;
    public float spread;
}
