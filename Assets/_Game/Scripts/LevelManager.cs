using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private ColorTypeSO colorTypeSO;
    private void Start()
    {
        SetCharactersColor();
    }

    private void SetCharactersColor()
    {
        //foreach (Character character in characters)
        //{
        //    MaterialType characterMaterialType = (MaterialType)Random.Range(0, System.Enum.GetValues(typeof(MaterialType)).Length);
        //    Material characterMaterial = ColorManager.Instance.color.GetColor(characterMaterialType);
        //    character.SetCharacterColor(characterMaterial, (int)characterMaterialType);
        //}

        if(characters.Length > System.Enum.GetValues(typeof(MaterialType)).Length)
        {
            Debug.LogError("Character over than material");
        }
        else
        {
            for (int i = 0; i < characters.Length; i++)
            {
                MaterialType characterMaterialType = (MaterialType)i;
                Material characterMaterial = ColorManager.Instance.color.GetColor(characterMaterialType);
                characters[i].SetCharacterColor(characterMaterial, (int)characterMaterialType);
            }
        }

       
    }
}
