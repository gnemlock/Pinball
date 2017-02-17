using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Flippers : MonoBehaviour 
{
		 public static int score = 0;
		 public HingeJoint leftFlipper, rightFlipper;
		 public UnityEngine.UI.Text text;
		 public GameObject ball;
		 public Vector3 position;
		 public float springSpeed = 100f;
		 public float springDamper = 3f;

		 public GameObject explosion;
		 public Transform explosionSpawn;

		 private const KeyCode leftFlipperButton = KeyCode.LeftArrow;
		 private const KeyCode rightFlipperButton = KeyCode.RightArrow;
		 private const string triggerName = "Trigger";

		 void Start()
		 {
					position = ball.transform.position;
		 }

		 void Update()
		 {
					text.text = "Score: " + score;

					if (Input.GetKeyDown (leftFlipperButton))
					{
							 JointSpring hingeSpring = leftFlipper.spring;
							 hingeSpring.spring = springSpeed;
							 hingeSpring.damper = springDamper;
							 hingeSpring.targetPosition = 60;
							 leftFlipper.spring = hingeSpring;
							 leftFlipper.useSpring = true;
					} 
					else if (Input.GetKeyUp (leftFlipperButton))
					{
							 JointSpring hingeSpring = leftFlipper.spring;
							 hingeSpring.spring = springSpeed;
							 hingeSpring.damper = springDamper;
							 hingeSpring.targetPosition = 0;
							 leftFlipper.spring = hingeSpring;
							 leftFlipper.useSpring = true;
					}

					if (Input.GetKeyDown (rightFlipperButton))
					{
							 JointSpring hingeSpring = rightFlipper.spring;
							 hingeSpring.spring = springSpeed;
							 hingeSpring.damper = springDamper;
							 hingeSpring.targetPosition = 60;
							 rightFlipper.spring = hingeSpring;
							 rightFlipper.useSpring = true;
					} 
					else if (Input.GetKeyUp (rightFlipperButton))
					{
							 JointSpring hingeSpring = rightFlipper.spring;
							 hingeSpring.spring = springSpeed;
							 hingeSpring.damper = springDamper;
							 hingeSpring.targetPosition = 0;
							 rightFlipper.spring = hingeSpring;
							 rightFlipper.useSpring = true;
					}

					if (Input.GetKeyDown (KeyCode.R))
					{
							 ball.transform.position = position;
					}

					if (Input.GetKeyDown (KeyCode.Space))
					{
							 Instantiate (explosion, explosionSpawn);
					}
		 }
}
