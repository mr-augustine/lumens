using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public enum Direction { N, NE, E, SE, S, SW, W, NW };
	public enum Quadrant { NW, NE, SE, SW };
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

	public void Notify(Block fallenBlock) {
		Debug.Log ("[Grid.Notify] " + fallenBlock.GetInstanceID() + 
		           "@ " + fallenBlock.GetPositionAsString() + " just fell.");

		AddBlock (fallenBlock);
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

		Debug.Log ("[ " + newSquare.ToString() + " added to grid[" + column + ", " + row +
		           "] from Grid(" + newSquare.transform.position.x + ", " +
		           newSquare.transform.position.y + ")]");
	}
}

