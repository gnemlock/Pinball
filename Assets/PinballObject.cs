﻿using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public class PinballObject : MonoBehaviour
{
    public float speed;
    public int score = 1860;
    public float spawnNumber;
    public float timer;
    public GameObject prefab;
    public Transform spawnPosition;
    public Modifier modifier;
    public Vector3 velocity;
    public Vector3 gravity;
    public float drag;
    public bool hold;

    public AudioSource soundEffect;
    
    private float delay = 0;
    
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }
    
    void OnValidate()
    {
        gameObject.name = modifier.ToString();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
    
    protected void OnCollisionEnter(Collision collision)
    {
    		CheckCollision (collision.gameObject);
    }

    protected void OnCollisionStay(Collision collision)
    {
        if(hold)
        {
            CheckCollision(collision.gameObject);
        }
    }
    
    protected void OnTriggerEnter(Collider other)
    {
    		CheckCollision (other.gameObject);
    }
    //TODO:point and shoot gui
    //
    private void CheckCollision(GameObject other)
    {
        if (other.CompareTag("Player"));
        {
            //GameManager.instance.AddScore(score);
            if(soundEffect)
            {
                soundEffect.Play();
            }

            Rigidbody rigidbody = other.GetComponent<Rigidbody> ();
            
            if(modifier == Modifier.spawnPlayer)
            {
                if(timer >= delay && prefab != null)
                {
                    GameObject otherBall = (GameObject)Instantiate<GameObject>(prefab);
                    otherBall.transform.position = spawnPosition.position;
                    otherBall.transform.rotation = spawnPosition.rotation;
                    timer = 0f;
                }
            }
            else if(modifier == Modifier.addPoints)
            {
                Flippers.score += (int)(score * rigidbody.velocity.magnitude);
            }
            else if(modifier == Modifier.bounce)
            {
                Debug.Log("bounce!");
            }
            else if(modifier == Modifier.addVelocity)
            {
                rigidbody.velocity += velocity * speed;
            }
            else if(modifier == Modifier.addDrag)
            {
                rigidbody.drag += drag * speed;
            }
            else if(modifier == Modifier.changeGravity)
            {
                Physics.gravity = gravity;
            }
            else if(modifier == Modifier.endGame)
            {
                GameManager.instance.GameOver();
            }
        }
    }
    
    [System.Serializable]
    public enum Modifier
    {
        spawnPlayer,
        addPoints,
        bounce,
        addVelocity,
        addDrag,
        changeGravity,
        doNothing,
        endGame
    }
    
    //TODO:Work on horizontal area
    //TODO:Work on game board piece prefabs
}
