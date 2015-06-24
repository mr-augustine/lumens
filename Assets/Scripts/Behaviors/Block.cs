using UnityEngine;
using System.Collections;

/// <summary>
/// The behavior for block pieces.
/// </summary>
public class Block : MonoBehaviour
{
	private BlockManager manager;
	[SerializeField]
	private float
		dropSpeed;
	[SerializeField]
	private GameObject[]
		squares;

	void OnEnable ()
	{
		manager = GameObject.Find ("BlockManager").GetComponent<BlockManager> ();
		dropSpeed = manager.GetInterval ();
		InvokeRepeating ("Drop", dropSpeed, dropSpeed);
	}

	/// <summary>
	/// Sets the block gameobject to active.
	/// </summary>
	public void Begin ()
	{
		gameObject.SetActive (true);
	}

	/// <summary>
	/// Calls the MoveDown method for each of the child squares.
	/// </summary>
	public void Drop ()
	{
		foreach (GameObject obj in squares) {
			obj.GetComponent<Square> ().MoveDown ();
		}
	}

	/// <summary>
	/// Calls the MoveLeft method for each of the child squares.
	/// </summary>
	public void MoveLeft ()
	{
		if (!HasCollided ())
			foreach (GameObject obj in squares) {
				obj.GetComponent<Square> ().MoveLeft ();
			}
	}

	/// <summary>
	/// Calls the MoveRight method for each of the child squares.
	/// </summary>
	public void MoveRight ()
	{
		if (!HasCollided ())
			foreach (GameObject obj in squares) {
				obj.GetComponent<Square> ().MoveRight ();
			}
	}

	/// <summary>
	/// Rotates the child squares' positions clockwise.
	/// </summary>
	public void RotateClockwise ()
	{
//		if (!HasCollided ())
	}

	/// <summary>
	/// Rotates the child squares' positions counter clockwise.
	/// </summary>
	public void RotateCounterClockwise ()
	{
//		if (!HasCollided ())
	}

	/// <summary>
	/// Determines whether any of the squares in the block have stopped moving.
	/// </summary>
	/// <returns><c>true</c> if this instance has collided; otherwise, <c>false</c>.</returns>
	private bool HasCollided ()
	{
		bool temp = false;
		foreach (GameObject obj in squares) {
			temp = temp || obj.GetComponent<Square> ().IsFinished ();
		}
		return temp;
	}

	/// <summary>
	/// Returns whether all squares in the block have stopped moving.
	/// </summary>
	/// <returns><c>true</c>, if done was alled, <c>false</c> otherwise.</returns>
	public bool AllDone ()
	{
		bool temp = true;
		foreach (GameObject obj in squares) {
			temp = temp && obj.GetComponent<Square> ().IsFinished ();
		}
		return temp;
	}


}
