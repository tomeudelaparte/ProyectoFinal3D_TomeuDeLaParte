using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        // Pool list
        pooledObjects = new List<GameObject>();

        // Create GameObject
        GameObject tmp;

        // Amount
        for (int i = 0; i < amountToPool; i++)
        {
            // Instance a GameObject
            tmp = Instantiate(objectToPool);

            // Desactivate it
            tmp.SetActive(false);

            // Add it to the pool
            pooledObjects.Add(tmp);
        }
    }

    // Return GameObject not active in hierarchy
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
