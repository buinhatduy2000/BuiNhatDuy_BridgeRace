using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBrick : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    public int numberColor;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            bool hasBricks = character.HasBricksInHolder();
            int characterNumColor = character.GetNumColor();
            Material characterMaterial = character.GetCurrentMaterial();

            if (hasBricks)
            {
                if (!_meshRenderer.enabled || (_meshRenderer.enabled && numberColor != characterNumColor))
                {
                    _meshRenderer.enabled = true;
                    _meshRenderer.material = characterMaterial;
                    numberColor = characterNumColor;

                    character.RemoveBrickFromHolder();
                }
            }
            else
            {
                if (_meshRenderer.enabled && numberColor != characterNumColor)
                {
                    Debug.Log("Cannot move");
                }
            }
        }
    }
}
