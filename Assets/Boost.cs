using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

		 public float boostMultiplier;

		 private void OnTriggerEnter(Collider other)
		 {
					if (other.CompareTag ("Player"))
					{
							 Rigidbody player = other.GetComponent<Rigidbody> ();

							 Vector3 velocity = player.velocity;

							 velocity *= boostMultiplier;

							 player.velocity = velocity;
					}
		 }
}
