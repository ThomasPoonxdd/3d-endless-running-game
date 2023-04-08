using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile(bool spawnItems) 
    {
        // spawn groundTile game object at nextSpawnPoint with no rotation
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        // get nextSpawnPoint from new groundTile game object
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems) 
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnCoins();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnTile(false);
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnTile(true);
        }
    }

}
