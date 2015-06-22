using UnityEngine;
using System.Collections;

/// <summary>
/// The behavior for block pieces.
/// </summary>
public class Block : MonoBehaviour
{
	private BlockManager manager;
	private float dropSpeed;
	private Ray rayDown, raySide;
	private Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
	}

	/// <summary>
	/// If not obstructed, moves the block one unit down.
	/// </summary>
	private void Drop ()
	{
		if (CheckBelow ()) {
			body.MovePosition (transform.position + Vector3.down);
		}
	}

	/// <summary>
	/// If not obstructed, moves the block one unit in the given direction.
	/// </summary>
	/// <param name="side">If set to <c>true</c> LEFT, <c>false</c> RIGHT.</param>
	public void MoveSide (bool side)
	{
		if (CheckSide (side)) {
			if (side) {
				body.MovePosition (transform.position + Vector3.left);
			} else {
				body.MovePosition (transform.position + Vector3.right);
			}
		}
	}

	/// <summary>
	/// Uses raycasting to check for obstacles below the block.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool CheckBelow ()
	{
		rayDown = new Ray (transform.position, Vector3.down);
		return !Physics.Raycast (rayDown, 1f);
	}
	
	/// <summary>
	/// Uses raycasting to check for obstacles in the given direction of the block.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	/// <param name="side">If set to <c>true</c> LEFT, <c>false</c> RIGHT.</param>
	private bool CheckSide (bool side)
	{
		if (side) {
			raySide = new Ray (transform.position, Vector3.left);
		} else {
			raySide = new Ray (transform.position, Vector3.right);
		}
		return !Physics.Raycast (raySide, 1f);
	}

	public bool IsFinished(){
		return true;
	}
}
