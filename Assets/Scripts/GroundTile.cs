using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleProb = 0.2f;
    [SerializeField] float leftRightPosition = 3.3f;


    public void SpawnObstacle()
    {
        // Choose which obstacle to spawn
        //GameObject obstacleToSpawn = obstaclePrefab;
        

        // Choose a random point to spawn the obstacle

        int count = 0;
        for (int obstacleSpawnIndex = 2; obstacleSpawnIndex < 5; obstacleSpawnIndex++)
        {
            
            GameObject obstacleToSpawn = obstaclePrefab;

            float prob = Random.Range(0f, 1f);
            float random = Random.Range(0f, 1f);
            if (random < tallObstacleProb)
            {
                obstacleToSpawn = tallObstaclePrefab;
            }

            if (prob > 0.5 & count < 2)
            {
                Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
                //Spawn the obstacle at the position
                // the obstacle will destory when the ground Tile is destroy
                Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
                count++;
            }

        }

        // spawn one obstacle if no enemy spawns
        if (count == 0)
        {
            float random = Random.Range(0f, 1f);
            GameObject obstacleToSpawn = obstaclePrefab;
            if (random < tallObstacleProb)
            {
                obstacleToSpawn = tallObstaclePrefab;
            }

            int obstacleSpawnIndex = Random.Range(2, 5);
            Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
            //Spawn the obstacle at the position
            // the obstacle will destory when the ground Tile is destroy
            Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);

        }
    }
    // Start is called before the first frame update


    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other) 
    {
        groundSpawner.SpawnTile(true);
        // Destroy gameObject 2 sec after player leave the trigger 
        Destroy(gameObject, 2);
    }
    private void Update() {
        //Vector3 forwardMove = new Vector3(0, 0, - speed * Time.fixedDeltaTime);
        //rb.MovePosition(rb.position + forwardMove);
    }

    public void SpawnCoins() 
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider) 
    {
        int random_XPos =  Random.Range(-1, 2);
        Vector3 point = new Vector3(
            random_XPos* leftRightPosition,
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
