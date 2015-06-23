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
	private void Drop ()
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
		foreach (GameObject obj in squares) {
			obj.GetComponent<Square> ().MoveLeft ();
		}
	}

	/// <summary>
	/// Calls the MoveRight method for each of the child squares.
	/// </summary>
	public void MoveRight ()
	{
		foreach (GameObject obj in squares) {
			obj.GetComponent<Square> ().MoveRight ();
		}
	}

	/// <summary>
	/// Rotates the child squares' positions clockwise.
	/// </summary>
	public void RotateClockwise ()
	{

	}

	/// <summary>
	/// Rotates the child squares' positions counter clockwise.
	/// </summary>
	public void RotateCounterClockwise ()
	{

	}

	/// <summary>
	/// Returns whether all squares of the block have stopped moving.
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
