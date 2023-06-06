using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Normal
}

[CreateAssetMenu(fileName = "Character", menuName = "Character/CharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    public int coolTime = 1;
    public float projectileSpeed = 1.0f;
    public float attackDelay = 1.0f;
    public int pAttackDelay = 0;
    public int projectileNum = 1;
    public int range = 100;
    public int strength = 5;
    public int healthPoint = 50;
    public int attackDuration = 100;
    public CharacterType type;
}
