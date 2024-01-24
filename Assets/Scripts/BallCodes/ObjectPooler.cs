using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject preFab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDict;

    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start() 
    {

        //initialize the dictionary
        poolDict = new Dictionary<string, Queue<GameObject>>();

        //for each pool created in the editor
        foreach(Pool pool in pools)
        {
            //instantiate all the objects from that pool and leave them inactive
             Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.preFab);

                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }

            //add the pool created in the dictionary
            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("The dictionary does not contain this key: " + tag);
            return null;
        }

        //get the object from the queue and set it to active
        GameObject objectToSpawn = poolDict[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        //puts the object back in the queue for when we need it again (he will not be instantiaed again)
        poolDict[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
