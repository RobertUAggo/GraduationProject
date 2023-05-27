using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Pool<T> where T : MonoBehaviour
{
    private int totalCount = 0;
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _freeQueue;
    public readonly UnityEvent<T> OnCreate = new UnityEvent<T>();
    public readonly UnityEvent<T> OnTake = new UnityEvent<T>();
    public readonly UnityEvent<T> OnBack = new UnityEvent<T>();
    private int _capacity;
    public int FreeCount => _freeQueue.Count;
    public Pool(T prefab, Transform parent, int capacity = 10)
    {
        _prefab = prefab;
        _parent = parent;
        _capacity = capacity;
        _freeQueue = new Queue<T>(_capacity);
    }
    public void Init()
    {
        for (int i = 0; i < _capacity; i++)
        {
            _freeQueue.Enqueue(CreateNew());
        }
    }
    private T CreateNew()
    {
        T newInstance = UnityEngine.Object.Instantiate(_prefab, _parent);
        newInstance.name = $"{_prefab.name} ({totalCount})";
        OnCreate.Invoke(newInstance);
        totalCount++;
        return newInstance;
    }
    public T Take()
    {
        T instance;
        if (_freeQueue.Count == 0) instance = CreateNew();
        else instance = _freeQueue.Dequeue();
        OnTake.Invoke(instance);
        return instance;
    }
    public void BackToQueue(T instance)
    {
        _freeQueue.Enqueue(instance);
        OnBack.Invoke(instance);
    }
}