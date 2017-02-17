using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
		 public Vector3 lastSideCollision;
		 public Vector3 lastAlignedSideCollision;
		 public float minimumCollisionMovement;
		 public float impulseNegation;
		 public Rigidbody rigidbody;
		 private float defaultMass;

		 public float massTimer;
		 public float temporaryMass;

		 private void Start()
		 {
					rigidbody = GetComponent<Rigidbody> ();
					defaultMass = rigidbody.mass;
		 }

		 private void OnCollisionEnter(Collision collision)
		 {
					if (collision.gameObject.tag == "Board")
					{
							 float distance = Vector3.Distance (lastAlignedSideCollision, transform.localPosition);
							 if (distance < minimumCollisionMovement)
							 {
										//TODO:Move ball off course so it does not bounce back and forth for ages
										//Debug.Log("Steep Bounce: " + lastAlignedSideCollision + " " + transform.position + " = " + distance);
										rigidbody.AddForce(rigidbody.velocity.normalized * -impulseNegation, ForceMode.Impulse);
										//rigidbody.mass = temporaryMass;
										//TODO:Check if this is stopping ball on wall
										//StartCoroutine (ResetMass ());
							 }

							 lastAlignedSideCollision = lastSideCollision;
							 lastSideCollision = transform.localPosition;
					}
		 }

		 private IEnumerator ResetMass()
		 {
					yield return new WaitForSeconds (massTimer);
					rigidbody.mass = defaultMass;
		 }
}
