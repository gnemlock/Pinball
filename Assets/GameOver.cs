using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	public GameManager gameManager;
	private static string playerTag = "Player";
	
	void OnCollisionEnter(Collision collision)
    {
    	Debug.Log("ping");
		if(collision.other.tag == playerTag)
		{
			gameManager.GameOver();
		}
    }

    void Start()
    {
        Debug.Log("ping");
    }
	//TODO:FixedUpdate delta time change in parallel with timescale
	//TODO: why isnt oncollision triggering?
}
