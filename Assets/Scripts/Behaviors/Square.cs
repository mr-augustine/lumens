using UnityEngine;
using System.Collections;

public enum SquareType
{
	WHITE,
	ORANG,
	SPC_W,
	SPC_O}
;

public class Square : MonoBehaviour
{
	[SerializeField]
	private SquareType
		type;
	private Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();

		Material mat = GetComponent<Renderer> ().material;
		switch (type) {
		case SquareType.WHITE:
			mat.color = Color.white;
			break;
		case SquareType.SPC_W:
			mat.color = Color.white;
			break;
		case SquareType.ORANG:
			mat.color = Color.red;
			break;
		case SquareType.SPC_O:
			mat.color = Color.red;
			break;
		}
	}

	#region BLOCK MOVEMENT

	/// <summary>
	/// Moves the square one unit up.
	/// </summary>
	public void MoveUp ()
	{
		body.MovePosition (transform.position + Vector3.up);
	}

	/// <summary>
	/// Moves the square one unit down.
	/// </summary>
	public void MoveDown ()
	{
		if (CheckDown ())
			body.MovePosition (transform.position + Vector3.down);
	}

	/// <summary>
	/// Moves the square one unit left.
	/// </summary>
	public void MoveLeft ()
	{
		if (CheckLeft ())
			body.MovePosition (transform.position + Vector3.left);
	}

	/// <summary>
	/// Moves the square one unit right.
	/// </summary>
	public void MoveRight ()
	{
		if (CheckRight ())
			body.MovePosition (transform.position + Vector3.right);
	}

	#endregion

	# region COLLISION DETECTION

	/// <summary>
	/// Checks if the square will collide with something beneath it.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool CheckDown ()
	{
		Ray ray = new Ray (transform.position, Vector3.down);
		return !Raycast (ray, .5f);
	}

	/// <summary>
	/// Checks if the square will collide with something to its left.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool CheckLeft ()
	{
		Ray ray = new Ray (transform.position, Vector3.left);
		return !Raycast (ray, .5f);
	}

	/// <summary>
	/// Checks if the square will collide with something to its right.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool CheckRight ()
	{
		Ray ray = new Ray (transform.position, Vector3.right);
		return !Raycast (ray, .5f);
	}

	/// <summary>
	/// Raycast the specified ray and distance.
	/// </summary>
	/// <param name="ray">Ray.</param>
	/// <param name="distance">Distance.</param>
	private bool Raycast (Ray ray, float distance)
	{
		return Physics.Raycast (ray, distance);
	}

	#endregion

	/// <summary>
	/// Gets the type of the square.
	/// </summary>
	/// <returns>The square type.</returns>
	public SquareType GetSquareType ()
	{
		return type;
	}

	/// <summary>
	/// Determines whether the square is finished.
	/// </summary>
	/// <returns><c>true</c> if this instance is finished; otherwise, <c>false</c>.</returns>
	public bool IsFinished ()
	{
		return !CheckDown ();
	}
}
