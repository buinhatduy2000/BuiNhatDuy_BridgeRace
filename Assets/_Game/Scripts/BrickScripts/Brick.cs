using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Renderer brickRenderer;
    [SerializeField] private int numColor;
    public delegate void BrickPickedUpAction(Brick brick);
    public event BrickPickedUpAction OnBrickPickedUp;

    public void SetBrickMaterial(Material material)
    {
        brickRenderer.material = material;
    }

    public void SetNumColor(int color)
    {
        numColor = color;
    }

    public int GetNumColor()
    {
        return numColor;
    }
    public void PickUp()
    {
        OnBrickPickedUp?.Invoke(this);
        Destroy(gameObject);
    }
}
