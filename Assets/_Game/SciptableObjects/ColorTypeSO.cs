using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Color Type")]
public class ColorTypeSO : ScriptableObject
{
    public Material[] TypeColor;
    public Material GetColor(MaterialType typeColor)
    {
        return TypeColor[(int)typeColor];
    }

}

public enum MaterialType
{
    Blue = 0,
    Green = 1,
    Red = 2,
    Yellow = 3,
}
