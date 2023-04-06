using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Renderer brickRenderer;
    [SerializeField] private Transform parentBrick;
    [SerializeField] private int numColor;
    [SerializeField] public Vector3 pos;

    private void Start()
    {
        MaterialType brickMaterialType = (MaterialType)numColor;
        Material brickMaterial = ColorManager.Instance.color.GetColor(brickMaterialType);
        SetBrickMaterial(brickMaterial);
        pos = this.transform.position;
    }

    public void SetBrickMaterial(Material material)
    {
        brickRenderer.material = material;
    }

    public int GetNumColor()
    {
        return numColor;
    }

    public void ReSpawnBrick()
    {
        this.transform.SetParent(parentBrick);
        this.gameObject.tag = "Brick";
        this.gameObject.transform.localPosition = pos;
        Quaternion rote = parentBrick.rotation;
        this.transform.rotation = rote;
    }
}
