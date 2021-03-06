﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	public float health;
	public float moveSpeed;
	public float posX;
	public float posY;
	public float cumPosX = 0, cumPosY = 0;
	public float damage;
	public GameObject doorEnter;
	public GameObject doorExit;

	Transform player;

	private Vector3 moveDir;

	public float amplitude;
	public float frequency;


	void Awake() {// IDK what awake does but it was in the tutoral lol
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		moveToPlayer ();

		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y + amplitude * Mathf.Sin(frequency * Time.time),this.transform.position.z);
	}

	void OnTriggerStay2D(Collider2D other) { //This will do ticks of damage every frame, however it stops until the player moves again at about 20 frames
		//Debug.Log (other);
		if(other.gameObject.tag.Equals("Player")) {
			other.gameObject.GetComponent<Player> ().takeDamage(damage);
		}
		//Destroy (this.gameObject);
	}

	public void takeDamage(float damage) {//death, damage, pain
		health -= damage;
		this.gameObject.GetComponentInChildren<Fade>().fade();
		this.gameObject.GetComponent<GUIscript> ().takeDamage ();
		if(health <= 0) {
			die();
		}
	}

	void die() {// :(
		doorEnter.GetComponent<Door>().doorDeath();
		doorExit.GetComponent<Door>().doorDeath();
		Destroy (this.gameObject);
	}

	public void moveToPlayer() { //Spent forever on this only to find a function that was easy 
		if(Global.playerAlive == true) {
			this.transform.position = Vector3.MoveTowards (transform.position,player.transform.position,moveSpeed * Time.deltaTime);

		}
	}

	public float getHealth(){
		return health;
	}
}


