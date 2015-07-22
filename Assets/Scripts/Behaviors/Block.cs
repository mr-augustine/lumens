using UnityEngine;
using System.Collections;

/// <summary>
/// The behavior for block pieces.
/// </summary>
public class Block : MonoBehaviour, IEnumerable
{
	public enum Spot { BotRight, BotLeft, TopLeft, TopRight };
	public static int NUMSQUARES = 4;
	private BlockManager manager;
	[SerializeField]
	private float
		dropSpeed;
	[SerializeField]
	private GameObject[]
		squares;
	private GameObject botLeft, botRight, topRight, topLeft;

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
		topLeft = squares  [0];
		topRight = squares [1];
		botLeft = squares [2];
		botRight = squares [3];
	}

	/// <summary>
	/// Calls the MoveDown method for each of the child squares.
	/// </summary>
	public void Drop ()
	{
		foreach (GameObject obj in squares) {
			if(obj != null)
				obj.GetComponent<Square> ().MoveDown ();
		}
	}

	/// <summary>
	/// Calls the MoveLeft method for each of the child squares.
	/// </summary>
	public void MoveLeft ()
	{
		if (!HasCollided ()) {
			if (!topLeft.GetComponent<Square> ().CanMoveLeft () && !botLeft.GetComponent<Square> ().CanMoveLeft ()) {
				foreach (GameObject obj in squares) {
					obj.GetComponent<Square> ().MoveLeft ();
				}
			}
		}
	}

	/// <summary>
	/// Calls the MoveRight method for each of the child squares.
	/// </summary>
	public void MoveRight ()
	{
		if (!HasCollided ()) {
			if (!topRight.GetComponent<Square> ().CanMoveRight () && !botRight.GetComponent<Square> ().CanMoveRight ()) {
				foreach (GameObject obj in squares) {
					obj.GetComponent<Square> ().MoveRight ();
				}
			}
		}
	}

	/// <summary>
	/// Calls the MoveDown method for each of the child squares.
	/// </summary>
	public void MoveDown ()
	{
		//if (!HasCollided ()) {
			CancelInvoke ();
			foreach (GameObject obj in squares) {
				obj.GetComponent<Square> ().MoveDown ();
			}
			InvokeRepeating ("Drop", dropSpeed, dropSpeed);
	//	}
	}

	/// <summary>
	/// Rotates the child squares' positions clockwise.
	/// </summary>
	public void RotateClockwise ()
	{
		if (!HasCollided ()) {
			topLeft.transform.Translate (new Vector3 (topRight.transform.position.x - topLeft.transform.position.x, 0, 0));
			topRight.transform.Translate (new Vector3 (0, botRight.transform.position.y - topRight.transform.position.y, 0));
			botRight.transform.Translate (new Vector3 (botLeft.transform.position.x - botRight.transform.position.x, 0, 0));
			botLeft.transform.Translate (new Vector3 (0, topLeft.transform.position.y - botLeft.transform.position.y, 0));
			
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
			topLeft.transform.Translate (new Vector3 (0, botLeft.transform.position.y - topLeft.transform.position.y, 0));
			botLeft.transform.Translate (new Vector3 (botRight.transform.position.x - botLeft.transform.position.x, 0, 0));
			botRight.transform.Translate (new Vector3 (0, topRight.transform.position.y - botRight.transform.position.y, 0));
			topRight.transform.Translate (new Vector3 (topLeft.transform.position.x - topRight.transform.position.x, 0, 0));

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

	/// <summary>
	/// Returns whether the block resides in the dead zone or not.
	/// </summary>
	/// <returns><c>true</c>, if in dead zone, <c>false</c> otherwise.</returns>
	public bool InDeadZone ()
	{
		bool temp = false;
		foreach (GameObject obj in squares) {
			temp = temp || obj.GetComponent<Square> ().InDeadZone ();
		}
		return temp;
	}

	public string GetPositionAsString() {
		return ("[" + this.transform.position.x + ", " +
				this.transform.position.y + ", " +
				this.transform.position.z + "]");
	}

	public GameObject[] GetSquaresArray() {
		return this.squares;
	}

	/// <summary>
	/// Identifies the Square positioned in the specified Spot for a Block.
	/// </summary>
	/// <returns>A reference to the Square at Spot.</returns>
	/// <param name="spot">Relative position within the Block.</param>
	public Square GetSquareAtSpot(Spot spot) {
		switch (spot) {
			case Spot.BotLeft:
				return botLeft.GetComponent<Square>();
			case Spot.BotRight:
				return botRight.GetComponent<Square>();
			case Spot.TopLeft:
				return topLeft.GetComponent<Square>();
			case Spot.TopRight:
				return topRight.GetComponent<Square>();
			default:
				Debug.Log("Received invalid Square Spot");
				return null;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return (IEnumerator) GetEnumerator();
	}

	public BlockEnum GetEnumerator() {
		return new BlockEnum(this);
	}

	public void RemoveSquare(Square s){
		for (int i = 0; i < 4; i++) {
			if(squares[i] != null && squares[i].GetInstanceID () == s.gameObject.GetInstanceID ()) {
				squares[i] = null;
				break;
			}
		}
		foreach (GameObject sq in squares) {
			if(sq != null)
				return;
		}
		Debug.Log ("About to destroy Block: " + this.GetInstanceID ());
		UnityEngine.Object.Destroy (this);
	}
}

/// <summary>
/// Enumerates all of a Block's Squares in a specific order.
/// This allows Blocks to be used in <c>foreach<c/c> loops
/// </summary>
public class BlockEnum: IEnumerator {
	Block _block;
	int position = -1;

	public BlockEnum(Block block) {
		_block = block;
	}

	public bool MoveNext() {
		position++;
		return (position < Block.NUMSQUARES);
	}

	public void Reset() {
		position = -1;
	}

	object IEnumerator.Current  {
		get {
			return Current;
		}
	}

	public Square Current {
		get {
			switch (position) {
				case 0:
					return _block.GetSquareAtSpot(Block.Spot.BotRight);
				case 1:
					return _block.GetSquareAtSpot(Block.Spot.BotLeft);
				case 2:
					return _block.GetSquareAtSpot(Block.Spot.TopLeft);
				case 3:
					return _block.GetSquareAtSpot(Block.Spot.TopRight);
				default:
					throw new System.InvalidOperationException();
			}
		}
	}
}