using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Character Character = other.gameObject.GetComponent<Character>();
            BrickSpawn.Instance.BrickSpawnNextPlane(Character.GetNumColor());
        }
    }
}
