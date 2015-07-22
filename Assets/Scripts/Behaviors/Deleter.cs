using System;
using System.Collections.Generic;
using UnityEngine;


public class Deleter
{

	private SweeperManager sweeper;
	private Grid theGrid;

	public Deleter (Grid grid, SweeperManager sweep)
	{
		theGrid = grid;
		sweeper = sweep;
	}

	public void Delete(int col){
		//Check all squares in col for cluster association and delete accordingly
		List<Square> squares = theGrid.GetSquaresInColumn (col);
		foreach (Square s in squares) {
			if(s.GetCluster() != null){
				if (s.GetBlock() != null)
					s.GetBlock().RemoveSquare(s);
				theGrid.RemoveSquare(s);
				//UnityEngine.Object.Destroy(s);
				s.gameObject.SetActive (false);
			}
		}
	}

}