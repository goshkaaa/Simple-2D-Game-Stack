using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box_Prefab;
    public float spawnAreaMinX = -1f;
    public float spawnAreaMaxX = -1f;
    public float spawnY = 5f;

    public void SpawnBox()
    {
        GameObject box_Obj = Instantiate(box_Prefab);

        float randomX = Random.Range(spawnAreaMinX, spawnAreaMaxX);
        Vector3 temp = new Vector3(randomX, spawnY, 0f);
        box_Obj.transform.position = temp;

        Debug.Log("Spawned box at: " + temp);
    }
}