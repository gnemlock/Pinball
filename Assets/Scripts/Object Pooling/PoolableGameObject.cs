/*
 * Created by Matthew Keating with direction from Vincent Black
 * All Rights Reserved
 */

using UnityEngine;

public class PoolableGameObject : MonoBehaviour, IPoolable
{
    /// <summary>The "Parent Pool" from where this object is retrieved and returned.</summary>
    public GameObjectPool pool { get; set; }

    protected void OnLevelWasLoaded()
    {
        ReturnToPool();
    }

    /// <summary>Attempts to return this <see cref="PoolableGameObject"/> to it's "Parent Pool".
    /// If there is no "Parent Pool", the game object will be destroyed. </summary>
    public void ReturnToPool()
    {
        if(pool != null)
        {
            pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
         }
}

public interface IPoolable
{
    GameObjectPool pool { get; set; }

    void ReturnToPool();

    void Enable();
    void Disable();
}