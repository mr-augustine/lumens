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

	public static int toCol(float colIn3DGridspace) {
		return Mathf.FloorToInt (colIn3DGridspace) + COLOFFSET;
	}

	public static int toRow(float rowIn3DGridspace) {
		return MAXROW - Mathf.FloorToInt (rowIn3DGridspace);
	}

	public static bool isValidCoord(int row, int column) {
		return (row >= MINROW && row <= MAXROW &&
		        column >= MINCOLUMN && column <= MAXCOLUMN);
	}

	public void Notify(Block fallenBlock) {
		Debug.Log ("[Grid.Notify] " + fallenBlock.GetInstanceID() + 
		           "@ " + fallenBlock.GetPositionAsString() + " just fell.");

		AddBlock (fallenBlock);
		EvaluateBlock (fallenBlock);
	}

	private void AddBlock(Block newBlock) {
		foreach (Square square in newBlock) {
			//Debug.Log (square.name);
			AddSquare (square);
		}
	}

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

	private Square GetNeighbor(Square origin, Direction where) {
		if (origin != null) {
			int targetRow = -1;
			int targetColumn = -1;

			// Remember: increment the row value to move lower 
			// in the grid and vice versa
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

	private void EvaluateBlock(Block block) {
		if (block != null) {
			foreach (Square square in block) {
				EvaluateSquare (square);
			}
		}
	}

	private void EvaluateSquare(Square square) {
		if (square != null) {
			foreach (Quadrant quadrant in System.Enum.GetValues (typeof(Quadrant))) {
				if (IsSameColor(square, quadrant) == false) {
					continue;
				}

				Debug.Log ("***Neighbors of " + square.ToString () + 
				           " in " + quadrant.ToString () + " can be deleted***");
			}
		}
	}
}

