using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {

	public Transform target;

	public float movmentSpeed = 1f;
	public float triggerSeekDistance = 6f;
	public float targetProximityDistance = 1f;

	public Animator animator;

	Path enemyPath;
	int currentWaypointIndex = 0;
	bool reachedEndOfPath = false;

	Seeker seeker;
	Rigidbody2D rigidBody;

	private Vector2 enemySpawnPosition;
	// Start is called before the first frame update
	void Start ()
	{
		seeker = GetComponent<Seeker> ();
		rigidBody = GetComponent<Rigidbody2D> ();

		enemySpawnPosition = rigidBody.position;

		InvokeRepeating ("UpdatePath", 0f, 0.6f);
	}

	private bool IsNextToTarget ()
	{
		return Vector2.Distance (target.position, rigidBody.position) < targetProximityDistance;
	}

	private bool IsTargetInSeekRange ()
	{
		return Vector2.Distance (target.position, rigidBody.position) < triggerSeekDistance;
	}

	private bool IsEnemyInSpawnLocation ()
	{
		return Vector2.Distance (enemySpawnPosition, rigidBody.position) < 0.1f;
	}

	void UpdatePath ()
	{
		if (seeker.IsDone ()) {
			if (IsTargetInSeekRange () && PlayerInvisibility.IsPlayerInvisible == false) {
				seeker.StartPath (rigidBody.position, target.position, OnPathComplete);
			} else {
				if (!IsEnemyInSpawnLocation ()) {
					seeker.StartPath (rigidBody.position, enemySpawnPosition, OnPathComplete);
				}
			}
		}
	}

	void OnPathComplete (Path newPath)
	{
		if (!newPath.error) {
			enemyPath = newPath;
			currentWaypointIndex = 0;
		}
	}

	void FixedUpdate ()
	{
		if (enemyPath == null) {
			return;
		}

		if (currentWaypointIndex >= enemyPath.vectorPath.Count) {
			reachedEndOfPath = true;

			StopAnimate ();
			return;
		} else {
			reachedEndOfPath = false;
		}

		Vector2 nextPathPoint = enemyPath.vectorPath [currentWaypointIndex];

		rigidBody.position = Vector2.MoveTowards (rigidBody.position, nextPathPoint, movmentSpeed * Time.fixedDeltaTime);

		Vector2 direction = (nextPathPoint - rigidBody.position).normalized;

		Animate (direction);

		float distance = Vector2.Distance (rigidBody.position, nextPathPoint);

		if (distance == 0) {
			currentWaypointIndex++;
		}

		if (currentWaypointIndex == enemyPath.vectorPath.Count - 1 && IsNextToTarget ()) {
			currentWaypointIndex = enemyPath.vectorPath.Count;
		}

	}

	private void Animate (Vector2 direction)
	{
		if (direction != Vector2.zero) {
			animator.SetFloat ("Horizontal", direction.x);
			animator.SetFloat ("Vertical", direction.y);
		}

		animator.SetFloat ("Speed", Mathf.Clamp (direction.magnitude, 0.0f, 1.0f));
	}

	private void StopAnimate ()
	{
		animator.SetFloat ("Horizontal", 0f);
		animator.SetFloat ("Vertical", -1f);
		animator.SetFloat ("Speed", 0.0f);
	}











	//Vector2 movement;
	//// Update is called once per frame
	//void Start ()
	//{
	//	rigidBody = GetComponent<Rigidbody2D> ();
	//}

	//void Update ()
	//{
	//	// Input
	//	movement.x = Input.GetAxisRaw ("Horizontal");
	//	movement.y = Input.GetAxisRaw ("Vertical");

	//	if (Mathf.Abs (movement.x) > Mathf.Abs (movement.y)) {
	//		movement.y = 0;
	//	} else {
	//		movement.x = 0;
	//	}

	//	Animate ();
	//}

	//void FixedUpdate ()
	//{
	//	// Movemnt
	//	rigidBody.MovePosition (rigidBody.position + movement * movmentSpeed * Time.fixedDeltaTime);
	//}

	//private void Animate ()
	//{
	//	if (movement != Vector2.zero) {
	//		animator.SetFloat ("Horizontal", movement.x);
	//		animator.SetFloat ("Vertical", movement.y);
	//	}

	//	animator.SetFloat ("Speed", Mathf.Clamp (movement.magnitude, 0.0f, 1.0f));
	//}

}
