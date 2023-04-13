using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/MonsterStatus")]
public class MonsterStatus : ScriptableObject
{
    public string   monsterName;
    public int      hp;
    public float    speed;
    public int      force;
    public float    hitSpeed;
}
