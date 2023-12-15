using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapmove : MonoBehaviour
{
    public MapSpawn spawn;
    public float speed = 10f;
    public Transform removeTR;
    public Transform spawnpoint;
    public Transform removepoint;
    private Vector3 movevec = Vector3.back;
    private bool cansummon;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] RemoveTag = GameObject.FindGameObjectsWithTag("Remove");

        if (RemoveTag.Length > 0)
        {
            // Finding RemoveTR
            removeTR = RemoveTag[0].transform;

            // Objectpolol
            //Debug.Log("Target Object Name: " + removeTR.name);
            //Debug.Log("Target Object Position: " + removeTR.position);
        }
        else
        {
            Debug.LogWarning("No object with tag 'E' found.");
        }
    }

    private void OnEnable()
    {
        cansummon = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameManager.Instance.cutrrentspeed;
        GetComponent<Transform>().Translate(movevec * speed * Time.deltaTime);

        if (spawn != null && removepoint.position.z <= spawn.spawnposition.position.z && cansummon)
        {
            cansummon = false;
            spawn.Spawn();
        }

        if (removepoint.position.z <= removeTR.position.z)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<MapSpawn>() != null)
        {
            spawn = collider.GetComponent<MapSpawn>();
        }
    }
}
