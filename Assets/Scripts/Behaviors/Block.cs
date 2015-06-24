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
	GameObject botLeft;
	GameObject botRight;
	GameObject topRight;
	GameObject topLeft;
	
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
		topLeft = squares[0];
		topRight = squares[1];
		botLeft = squares[2];
		botRight = squares[3];
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
		if (!HasCollided ()) {
			topLeft.transform.Translate (new Vector3 (topRight.transform.position.x-topLeft.transform.position.x, 0, 0));
			topRight.transform.Translate (new Vector3 (0, botRight.transform.position.y-topRight.transform.position.y,0));
			botRight.transform.Translate (new Vector3 (botLeft.transform.position.x-botRight.transform.position.x, 0, 0));
			botLeft.transform.Translate (new Vector3 (0, topLeft.transform.position.y-botLeft.transform.position.y,0));
			
			GameObject temp = topRight;
			topRight = topLeft;
			topLeft = botLeft;
			botLeft = botRight;
			botRight = temp;
					
		}
	}

	/// <summary>
	/// Rotates the child squares' positions counter clockwise.
	/// </summary>
	public void RotateCounterClockwise ()
	{
		if (!HasCollided ()) {
			topLeft.transform.Translate (new Vector3 (0, botLeft.transform.position.y-topLeft.transform.position.y, 0));
			botLeft.transform.Translate (new Vector3 (botRight.transform.position.x-botLeft.transform.position.x,0,0));
			botRight.transform.Translate (new Vector3 (0,topRight.transform.position.y-botRight.transform.position.y, 0));
			topRight.transform.Translate (new Vector3 (topLeft.transform.position.x-topRight.transform.position.x,0,0));
			
			
			
			GameObject temp = topLeft;
			topLeft = topRight;
			topRight = botRight;
			botRight = botLeft;
			botLeft = temp;
			
		}
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
