using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollider : MonoBehaviour 
{
		 public int score = 10;

		 void OnCollisionEnter(Collision collision)
		 {
					if (collision.gameObject.tag == "Player")
					{
							 Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody> ();
							 Flippers.score += score * (int)rigidbody.velocity.magnitude;
							 Debug.Log ((int)rigidbody.velocity.magnitude);
					}
		 }
}
