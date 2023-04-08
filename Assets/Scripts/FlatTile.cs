using UnityEngine;

public class FlatTile : MonoBehaviour
{


    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject, 2);
    }



}
