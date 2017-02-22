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
		 public Transform focus;
		 //public float[] allowXFlags;
		 public float massTimer;
		 public float temporaryMass;

		 private Player instance;

		 private void Awake()
		 {
					if (instance == null)
					{
							 instance = this;
					} else if (instance != this)
					{
							 Destroy (this);
					}
		 }

		 private void Start()
		 {
					rigidbody = GetComponent<Rigidbody> ();
					defaultMass = rigidbody.mass;

					SetFocus ();
		 }

		 private void Update()
		 {
					if (Input.GetKeyDown (KeyCode.L))
					{
							 Debug.Log ("velocity:" + rigidbody.velocity + " drag:" + rigidbody.drag + " " + rigidbody.mass + " at gravity " + Physics.gravity);
					}

					SetFocus ();
		 }

		 void SetFocus()
		 {
					if (focus)
					{
							 focus.position = new Vector3 (transform.position.x, transform.position.y, 1.0f);
					} 
					else
					{
							 Debug.Log ("No Focus");
					}
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

//TODO: Fix camera to follow player up and down; figure out medium for multiple balls
//TODO: Setup delegates for generic collision functions
//TODO: Setup failsafe if ball gets stuck