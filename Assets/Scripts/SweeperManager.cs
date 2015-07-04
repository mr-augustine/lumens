using UnityEngine;
using System.Collections;

public class SweeperManager : MonoBehaviour
{

	private bool active;
	private Vector3 moveDirection;
	private Vector3 startingPosition;

	void Start ()
	{
		Invoke ("Begin", 1);
		moveDirection = Vector3.right;
		startingPosition = transform.position;
	}

	void Update ()
	{
		if (!active)
			return;
		transform.Translate (moveDirection * .05f);
	}

	void OnCollisionEnter (Collision col)
	{
		transform.position = startingPosition;
	}

	private void Begin ()
	{
		active = true;
	}
}
