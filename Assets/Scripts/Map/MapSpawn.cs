using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{
    public GameObject[] maps;
    public Transform spawnposition;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void Spawn()
    {
        //(int)Random.Range(0, maps.Length)
        GameObject map = GameManager.Instance.MapSpawner.Get((int)Random.Range(0, maps.Length));
        map.transform.position = transform.position;
        Vector3 offset =  spawnposition.position - map.GetComponent<Mapmove>().spawnpoint.position;
        map.transform.position = transform.position + offset;
        map.transform.rotation = transform.rotation;

    }

}
