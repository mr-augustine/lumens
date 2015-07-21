using UnityEngine;
using System.Collections;

public enum SquareType
{
	LIGHT,
	DARK,
	SPC_LIGHT,
	SPC_DARK,
	CLUSTER,	// The Color displayed when the square joins a Cluster
	ORIGINAL   	// A shortcut to specify the Color given to the 
				// Square when it was first initialized
}
;

public class Square : MonoBehaviour
{
	[SerializeField]
	private SquareType
		type;
	private Rigidbody body;

	private int gridRow = 0;
	private int gridColumn = 0;
	private Cluster cluster;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();

		Material mat = GetComponent<Renderer> ().material;
		switch (type) {
		case SquareType.LIGHT:
			mat.color = Color.white;
			break;
		case SquareType.SPC_LIGHT:
			mat.color = Color.white;
			break;
		case SquareType.DARK:
			mat.color = Color.red;
			break;
		case SquareType.SPC_DARK:
			mat.color = Color.red;
			break;
		}

		cluster = null;
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
		if (RaycastDown ())
			body.MovePosition (transform.position + Vector3.down);
	}

	/// <summary>
	/// Moves the square one unit left.
	/// </summary>
	public void MoveLeft ()
	{
		if (RaycastLeft ())
			body.MovePosition (transform.position + Vector3.left);
	}

	/// <summary>
	/// Moves the square one unit right.
	/// </summary>
	public void MoveRight ()
	{
		if (RaycastRight ())
			body.MovePosition (transform.position + Vector3.right);
	}

	#endregion

	# region COLLISION DETECTION

	/// <summary>
	/// Raycasts beneath the square to detect possible collisions.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool RaycastDown ()
	{
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hit;
		float distance = .5f;
		if (Physics.Raycast (ray, out hit, distance)) {
			Square tempSq;
			if ((tempSq = hit.transform.gameObject.GetComponent<Square> ()) != null) {
				return !tempSq.IsFinished ();
			} else {
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Raycasts to the left of the square to detect possible collisions.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool RaycastLeft ()
	{
		Ray ray = new Ray (transform.position, Vector3.left);
		RaycastHit hit;
		float distance = .5f;
		if (Physics.Raycast (ray, out hit, distance)) {
			Square tempSq;
			if ((tempSq = hit.transform.gameObject.GetComponent<Square> ()) != null) {
				if (tempSq.IsFinished ()) {
					return false;
				} else {
					return !tempSq.CanMoveLeft ();
				}
			} else {
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Raycasts to the right of the square to detect possible collisions.
	/// </summary>
	/// <returns><c>true</c>, if all clear, <c>false</c> otherwise.</returns>
	private bool RaycastRight ()
	{
		Ray ray = new Ray (transform.position, Vector3.right);
		RaycastHit hit;
		float distance = .5f;
		if (Physics.Raycast (ray, out hit, distance)) {
			Square tempSq;
			if ((tempSq = hit.transform.gameObject.GetComponent<Square> ()) != null) {
				if (tempSq.IsFinished ()) {
					return false;
				} else {
					return !tempSq.CanMoveRight ();
				}
			} else {
				return false;
			}
		}
		return true;
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
	/// Determines whether the square is finished moving.
	/// </summary>
	/// <returns><c>true</c> if this instance is finished; otherwise, <c>false</c>.</returns>
	public bool IsFinished ()
	{
		return !RaycastDown ();
	}

	/// <summary>
	/// Determines whether this square can move left.
	/// </summary>
	/// <returns><c>true</c> if this instance can move left; otherwise, <c>false</c>.</returns>
	public bool CanMoveLeft ()
	{
		return !RaycastLeft ();
	}

	/// <summary>
	/// Determines whether this square can move right.
	/// </summary>
	/// <returns><c>true</c> if this instance can move right; otherwise, <c>false</c>.</returns>
	public bool CanMoveRight ()
	{
		return !RaycastRight ();
	}

	public bool InDeadZone ()
	{
		return transform.position.y > 9.5;
	}

	public int GetGridRow() {
		return gridRow;
	}

	public void SetGridRow(int row) {
		gridRow = row;
	}

	public int GetGridColumn() {
		return gridColumn;
	}

	public void SetGridColumn(int column) {
		gridColumn = column;
	}

	/// <summary>
	/// Sets the grid coordinate for the Square. This function is
	/// called by the Grid whenever a Square reaches a terminating
	/// position within the Grid.
	/// </summary>
	/// <param name="row">Row index.</param>
	/// <param name="column">Column index.</param>
	public void SetGridCoord(int row, int column) {
		SetGridRow (row);
		SetGridColumn(column);
	}

	public void PresentColor() {
		// Use this function to change the color of the Square
		Material mat = GetComponent<Renderer> ().material;
		switch (type) {
		case SquareType.LIGHT:
			mat.color = Color.grey;
			break;
		case SquareType.SPC_LIGHT:
			mat.color = Color.grey;
			break;
		case SquareType.DARK:
			mat.color = Color.magenta;
			break;
		case SquareType.SPC_DARK:
			mat.color = Color.magenta;
			break;
		}
	}

	public Cluster GetCluster() {
		return cluster;
	}

	public void SetCluster(Cluster cluster) {
		this.cluster = cluster;
	}

	public override string ToString ()
	{
		return base.ToString () + ": " + type.ToString ();
	}

	public Block GetBlock(){
		return this.transform.parent.gameObject.GetComponent <Block>();
	}
}
