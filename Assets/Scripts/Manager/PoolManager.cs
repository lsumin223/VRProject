using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] prefabs;
    public List<GameObject>[] pools;

    void Start()
    {
        pools = new List<GameObject>[prefabs.Length];
        MakePool();
    }

    void MakePool()
    {
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        
        foreach(GameObject item in pools[index])
        {
            //Debug.Log(item);
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

       if(!select)
        {
            select = Instantiate(prefabs[index]);
            pools[index].Add(select);
        }

        return select;

    }
    
}
