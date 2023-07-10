using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterName
{
    Normal_Monster, Shield_Monster1, Shield_Monster2,
    Shield_Monster3, Aerial_Monster, Fast_Monster, Jump_Monster,
    Smart_Monster, Mani_Monster,
}

public enum MonsterType
{
    Normal, Aerial, Unique
}

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/MonsterStatus")]
public class MonsterStatus : ScriptableObject
{
    public string       monsterName;
    public int          hp;
    public float        speed;
    public int          force;
    public float        hitSpeed;
    public float        attackDistance = 1f;
    public MonsterType  type;
}
