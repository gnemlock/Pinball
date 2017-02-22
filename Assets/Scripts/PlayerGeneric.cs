using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGeneric : PoolableGameObject 
{
    public Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
