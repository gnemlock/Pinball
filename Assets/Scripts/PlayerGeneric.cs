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

//TODO:Use custom inspector to only draw properties relevant to selected function in PinballObject
//TODO:Add collision fields to apply sound filters to main music; test out
