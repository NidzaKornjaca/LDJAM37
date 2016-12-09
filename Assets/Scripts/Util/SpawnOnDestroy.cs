using UnityEngine;
using System.Collections;

public class SpawnOnDestroy : MonoBehaviour {
    public GameObject toSpawn;

    void OnDestroy() {
        if (toSpawn != null)
            Instantiate(toSpawn, transform.position, transform.rotation);
        else Debug.Log("SpawnOnDestroy on " + transform.name + " has no toSpawn prefab");
    }
}
