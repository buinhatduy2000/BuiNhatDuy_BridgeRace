using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public static BrickSpawn Instance;

    public Transform SpawnPointLv1;
    public Transform SpawnPointLv2;

    public Transform TranformParent_Blue;
    public Transform TranformParent_Green;
    public Transform TranformParent_Red;
    public Transform TranformParent_Yellow;

    private void Start()
    {
        Instance = this;

        TranformParent_Blue.SetParent(SpawnPointLv1);
        TranformParent_Blue.localPosition = SpawnPointLv1.position;
        TranformParent_Green.SetParent(SpawnPointLv1);
        TranformParent_Green.localPosition = SpawnPointLv1.position;
        TranformParent_Red.SetParent(SpawnPointLv1);
        TranformParent_Red.localPosition = SpawnPointLv1.position;
        TranformParent_Yellow.SetParent(SpawnPointLv1);
        TranformParent_Yellow.localPosition = SpawnPointLv1.position;
    }

    public void BrickSpawnNextPlane(int numColor)
    {
       switch (numColor){
            case 0:
                TranformParent_Blue.SetParent(SpawnPointLv2);
                TranformParent_Blue.localPosition = SpawnPointLv2.localPosition;
                break;
            case 1:
                TranformParent_Green.SetParent(SpawnPointLv2);
                TranformParent_Green.localPosition = SpawnPointLv2.localPosition;
                break;
            case 2:
                TranformParent_Red.SetParent(SpawnPointLv2);
                TranformParent_Red.localPosition = SpawnPointLv2.localPosition;
                break;
            case 3:
                TranformParent_Yellow.SetParent(SpawnPointLv2);
                TranformParent_Yellow.localPosition = SpawnPointLv2.localPosition;
                break;
            default:
                Debug.Log("=)))");
                break;
        }
    }
}
