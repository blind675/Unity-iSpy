using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movmentSpeed = 5f;
	public Rigidbody2D rigidBody;
	public Animator animator;

	public Joystick joystick;

	Vector2 movement;
	// Update is called once per frame
	void Update ()
	{
		// Input
		//movement.x = Input.GetAxisRaw ("Horizontal");
		//movement.y = Input.GetAxisRaw ("Vertical");

		movement.x = joystick.Horizontal;
		movement.y = joystick.Vertical;

		// If player will move deactivate invisibility
		if (DidPlayerMove () == true) {

			PlayerInvisibility.IsPlayerInvisible = false;
		}

		Animate ();
	}

	private bool DidPlayerMove ()
	{
		return movement != Vector2.zero;
	}

	void FixedUpdate ()
	{
		// Movemnt
		rigidBody.MovePosition (rigidBody.position + movement * movmentSpeed * Time.fixedDeltaTime);
	}

	private void Animate ()
	{
		if (movement != Vector2.zero) {
			animator.SetFloat ("Horizontal", movement.x);
			animator.SetFloat ("Vertical", movement.y);
		}

		animator.SetFloat ("Speed", Mathf.Clamp (movement.magnitude, 0.0f, 1.0f));

		animator.SetBool ("Invisible", PlayerInvisibility.IsPlayerInvisible);
	}
}
