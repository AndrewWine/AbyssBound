using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Queue<T> objects = new Queue<T>();

    public ObjectPool(T prefab, int initialCapacity)
    {
        this.prefab = prefab;
        for (int i = 0; i < initialCapacity; i++)
        {
            CreateNewObject();
        }
    }

    private void CreateNewObject()
    {
        T obj = GameObject.Instantiate(prefab);
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

    public T Get()
    {
        if (objects.Count > 0)
        {
            T obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool is empty. Creating a new instance.");
            CreateNewObject();
            return Get();
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

 


}



