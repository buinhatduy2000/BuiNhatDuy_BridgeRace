using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int numberOfRows = 5;
    [SerializeField] private int numberOfColumns = 5;
    [SerializeField] private float spacingX = 2f;
    [SerializeField] private float spacingZ = 2f;

    private void Start()
    {
        SpawnBricks();
    }
    private void SpawnBricks()
    {
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                Vector3 brickPosition = new Vector3(transform.position.x + i * spacingX, transform.position.y, transform.position.z + j * spacingZ);

                GameObject spawnedBrick = Instantiate(brickPrefab, brickPosition, Quaternion.identity);
                spawnedBrick.transform.SetParent(transform);

                MaterialType brickMaterialType = (MaterialType)Random.Range(0, System.Enum.GetValues(typeof(MaterialType)).Length);
                Material brickMaterial = ColorManager.Instance.color.GetColor(brickMaterialType);
                spawnedBrick.GetComponent<Brick>().SetBrickMaterial(brickMaterial);
                spawnedBrick.GetComponent<Brick>().SetNumColor((int)brickMaterialType);

                Brick brick = spawnedBrick.GetComponent<Brick>();
                brick.SetBrickMaterial(brickMaterial);
                brick.SetNumColor((int)brickMaterialType);
                brick.OnBrickPickedUp += Brick_OnBrickPickedUp;
            }
        }
    }
    private void Brick_OnBrickPickedUp(Brick brick)
    {
        Vector3 brickPosition = brick.transform.position;
        Quaternion brickRotation = brick.transform.rotation;
        Transform brickParent = brick.transform.parent;
        StartCoroutine(ReSpawnBrick(brickPosition, brickRotation, brickParent, 2f));
    }

    private IEnumerator ReSpawnBrick(Vector3 position, Quaternion rotation, Transform parent, float delay)
    {
        yield return new WaitForSeconds(delay);

        MaterialType brickMaterialType = (MaterialType)Random.Range(0, System.Enum.GetValues(typeof(MaterialType)).Length);
        Material brickMaterial = ColorManager.Instance.color.GetColor(brickMaterialType);

        GameObject spawnedBrick = Instantiate(brickPrefab, position, rotation);
        spawnedBrick.transform.SetParent(parent);

        Brick newBrick = spawnedBrick.GetComponent<Brick>();
        newBrick.SetBrickMaterial(brickMaterial);
        newBrick.SetNumColor((int)brickMaterialType);
        newBrick.OnBrickPickedUp += Brick_OnBrickPickedUp;
    }
}
