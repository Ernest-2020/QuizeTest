using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class Pool<T> where T : Component
{
    private readonly Transform _container;
    private readonly T[] _prefabs;

    public List<T> Objects { get; private set; }

    public Pool(Transform container, int startCount, params T[] prefabs)
    {
        _container = container;
        _prefabs = prefabs;
        InitializePool(startCount);
    }

    private void InitializePool(int count)
    {
        Objects = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private bool HasFreeObject(out T obj)
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            GameObject gameObject = Objects[i].gameObject;
            if (!gameObject.activeInHierarchy)
            {
                obj = Objects[i];
                gameObject.SetActive(true);
                return true;
            }
        }
        obj = null;
        return false;
    }

    private void AddObjectInPool(T addableObject,bool isActiveByDefault)
    {
        addableObject.gameObject.SetActive(isActiveByDefault);
        Objects.Add(addableObject);
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        T createObject = Object.Instantiate(_prefabs[Random.Range(0, _prefabs.Length)],_container);
        AddObjectInPool(createObject,isActiveByDefault);
        return createObject;
    }

    public T GetFreeObject(bool isActive = true)
    {
        if (HasFreeObject(out T element))
        {
            return element;
        }
        else
        {
            return CreateObject(isActive);
        }
    }
}