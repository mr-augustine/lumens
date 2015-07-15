using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public enum Direction { N, NE, E, SE, S, SW, W, NW };
	public enum Quadrant { SW, NW, NE, SE };
	public const int MINCOLUMN = 0;
	public const int MAXCOLUMN = 15;
	public const int MINROW = 0;
	public const int MAXROW = 9;

	public const int COLOFFSET = 8;
	public const int ROWOFFSET = 11;

	private Square[,] grid;

	// Use this for initialization
	void Start ()
	{
		grid = new Square[Grid.MAXROW + 1, Grid.MAXCOLUMN + 1];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	/// <summary>
	/// Converts a rigidbody's horizontal component from the scene's 3D space
	/// representation to a column index in the 2D grid representation.
	/// </summary>
	/// <returns>The column index</returns>
	/// <param name="column3D">x-component of the object's position in the scene</param>
	public static int toCol(float column3D) {
		return Mathf.FloorToInt (column3D) + COLOFFSET;
	}

	/// <summary>
	/// Converts a rigidbody's vertical component from the scene's 3D space
	/// representation to a row index in the 2D grid representation.
	/// </summary>
	/// <returns>The row index</returns>
	/// <param name="row3D">y-component of the object's position in the scene</param>
	public static int toRow(float row3D) {
		return MAXROW - Mathf.FloorToInt (row3D);
	}

	/// <summary>
	/// Determines whether the specified row-column position is valid
	/// </summary>
	/// <returns><c>true</c>, if the coordinate is valid, <c>false</c> otherwise.</returns>
	/// <param name="row">Row index.</param>
	/// <param name="column">Column index.</param>
	public static bool isValidCoord(int row, int column) {
		return (row >= MINROW && row <= MAXROW &&
		        column >= MINCOLUMN && column <= MAXCOLUMN);
	}

	/// <summary>
	/// Called by a Block to notify the Grid that it finished falling. Bootstraps
	/// the process of evaluating a recently-fallen block.
	/// </summary>
	/// <param name="fallenBlock">A block that finished falling.</param>
	public void Notify(Block fallenBlock) {
		Debug.Log ("[Grid.Notify] " + fallenBlock.GetInstanceID() + 
		           "@ " + fallenBlock.GetPositionAsString() + " just fell.");

		AddBlock (fallenBlock);
		EvaluateBlock (fallenBlock);
	}

	/// <summary>
	/// Adds the specified block to the 2D grid.
	/// </summary>
	/// <param name="newBlock">A new block.</param>
	private void AddBlock(Block newBlock) {
		foreach (Square square in newBlock) {
			//Debug.Log (square.name);
			AddSquare (square);
		}
	}

	/// <summary>
	/// Adds the specified square to the 2D grid.
	/// </summary>
	/// <param name="newSquare">A new square.</param>
	private void AddSquare(Square newSquare) {
		int row;
		int column;

		column = Grid.toCol(newSquare.transform.position.x);
		row = Grid.toRow(newSquare.transform.position.y);

		grid[row, column] = newSquare;
		newSquare.SetGridCoord(row, column);

		Debug.Log ("[ " + newSquare.ToString() + " added to grid[" + column + ", " + row +
		           "] from Grid(" + newSquare.transform.position.x + ", " +
		           newSquare.transform.position.y + ")]");
	}

	/// <summary>
	/// Identifies the Square adjacent to the given Square in the
	/// specified Direction
	/// </summary>
	/// <returns>If present, a reference to the Square's neighbor.
	/// <c>null</c> otherwise</returns>
	/// <param name="origin">The square of interest.</param>
	/// <param name="where">The relative direction to look for a neighbor.</param>
	private Square GetNeighbor(Square origin, Direction where) {
		if (origin != null) {
			int targetRow = -1;
			int targetColumn = -1;

			// Note: We increment the row value to move lower 
			// in the 2D grid and vice versa.
			switch (where) {
			case Direction.S:
				targetRow = origin.GetGridRow () + 1;
				targetColumn = origin.GetGridColumn ();
				break;
			case Direction.SW:
				targetRow = origin.GetGridRow() + 1;
				targetColumn = origin.GetGridColumn() - 1;
				break;
			case Direction.W:
				targetRow = origin.GetGridRow ();
				targetColumn = origin.GetGridColumn() - 1;
				break;
			case Direction.NW:
				targetRow = origin.GetGridRow() - 1;
				targetColumn = origin.GetGridColumn() - 1;
				break;
			case Direction.N:
				targetRow = origin.GetGridRow() - 1;
				targetColumn = origin.GetGridColumn();
				break;
			case Direction.NE:
				targetRow = origin.GetGridRow() - 1;
				targetColumn = origin.GetGridColumn() + 1;
				break;
			case Direction.E:
				targetRow = origin.GetGridRow();
				targetColumn = origin.GetGridColumn() + 1;
				break;
			case Direction.SE:
				targetRow = origin.GetGridRow() + 1;
				targetColumn = origin.GetGridColumn () + 1;
				break;
			default:
				Debug.Log ("Unexpected Square Direction received. " +
				           "Unable to calculate neighbor grid position.");
				break;
			}


			if (isValidCoord (targetRow, targetColumn)) {
				return grid[targetRow, targetColumn];
			}
		}

		return null;
	}

	/// <summary>
	/// Determines whether a Square and all of its neigbors in the 
	/// specified Quadrant are the same color.
	/// </summary>
	/// <returns><c>true</c> if all Squares in the quadrant are the same Color; 
	/// otherwise, <c>false</c>.</returns>
	/// <param name="origin">The Square of interest.</param>
	/// <param name="where">The Quadrant relative to the Square.</param>
	private bool IsSameColor(Square origin, Quadrant where) {
		if (origin != null) {
			bool isSameColor = true;
			SquareType originColor = origin.GetSquareType();
			Square[] neighbors = {null, null, null};

			switch (where) {
			case Quadrant.SW:
				neighbors[0] = GetNeighbor(origin, Direction.S);
				neighbors[1] = GetNeighbor(origin, Direction.SW);
				neighbors[2] = GetNeighbor(origin, Direction.W);
				break;
			case Quadrant.NW:
				neighbors[0] = GetNeighbor(origin, Direction.W);
				neighbors[1] = GetNeighbor(origin, Direction.NW);
				neighbors[2] = GetNeighbor(origin, Direction.N);
				break;
			case Quadrant.NE:
				neighbors[0] = GetNeighbor(origin, Direction.N);
				neighbors[1] = GetNeighbor(origin, Direction.NE);
				neighbors[2] = GetNeighbor(origin, Direction.E);
				break;
			case Quadrant.SE:
				neighbors[0] = GetNeighbor(origin, Direction.E);
				neighbors[1] = GetNeighbor(origin, Direction.SE);
				neighbors[2] = GetNeighbor(origin, Direction.S);
				break;
			default:
				Debug.Log ("Unexpected Square Quadrant received. " +
				           "Unable to identify neighbors' colors.");

				return false;
			}

			foreach (Square neighbor in neighbors) {
				if (neighbor == null) {
					return false;
				}

				isSameColor &= (neighbor.GetSquareType().Equals (originColor));
			}

			if (isSameColor == true) {
				Debug.Log ("All neighbors are the same color!");
			}

			return isSameColor;
		}

		return false;
	}

	/// <summary>
	/// Evaluates the Squares in the specified Block for clustering.
	/// </summary>
	/// <param name="block">The Block of interest.</param>
	private void EvaluateBlock(Block block) {
		if (block != null) {
			foreach (Square square in block) {
				EvaluateSquare (square);
			}
		}
	}

	/// <summary>
	/// Evaluates the specified Square for clustering.
	/// </summary>
	/// <param name="square">The Square of interest.</param>
	private void EvaluateSquare(Square square) {
		if (square != null) {

			// Examine every Square in every Quadrant for the
			// Square of interest
			foreach (Quadrant quadrant in System.Enum.GetValues (typeof(Quadrant))) {
				if (IsSameColor(square, quadrant) == false) {
					continue;
				}

				// Here we would notify the Deleter to add the Square and its
				// neigbors to a Cluster
				Debug.Log ("***Neighbors of " + square.ToString () + 
				           " in " + quadrant.ToString () + " can be deleted***");
			}
		}
	}
}
