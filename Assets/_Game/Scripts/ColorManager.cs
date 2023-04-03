using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public ColorTypeSO color;
    public static ColorManager Instance;
    private void Awake()
    {
        Instance = this;
    }
}
