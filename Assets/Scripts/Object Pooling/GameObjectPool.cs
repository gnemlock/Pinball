/*
 * Created by Matthew Keating with direction from Vincent Black
 * All Rights Reserved
 */

using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public int AvailableObjectsCount { get { return availableObjects.Count; } }

    private List<PoolableGameObject> availableObjects = new List<PoolableGameObject>();
    private PoolableGameObject objectPrefab;

    public GameObjectPool(PoolableGameObject objectPrefab)
    {
        this.objectPrefab = objectPrefab;
    }

    public PoolableGameObject GetObject()
    {
        Debug.Log(availableObjects);
        PoolableGameObject pooledGameObject;
        int lastAvailableIndex = availableObjects.Count - 1;

        if(lastAvailableIndex >= 0)
        {
            // If the last available index is a valid index, there are game objects available.
            pooledGameObject = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            pooledGameObject.Enable();
        }
        else
        {
            // Else, the last available index is invalid, so we need to create a new game object.
            pooledGameObject = Instantiate<PoolableGameObject>(objectPrefab);
            //TODO:This is not passing the instance reference into our local reference OH NOOOE
            pooledGameObject.transform.SetParent(transform);
            pooledGameObject.pool = this;
        }

        return pooledGameObject;
    }

    public void AddObject(PoolableGameObject poolableObject)
    {
        poolableObject.Disable();
        availableObjects.Add(poolableObject);
    }
}
