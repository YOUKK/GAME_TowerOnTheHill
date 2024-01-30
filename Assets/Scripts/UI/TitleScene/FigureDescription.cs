using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/FigureDescription")]
public class FigureDescription : ScriptableObject
{
    public Sprite figureSprite;
    public string figureName;
    [TextArea(1, 6)]
    public string figureDescription;
}
