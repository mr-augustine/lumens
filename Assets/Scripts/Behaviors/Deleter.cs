using System;
using System.Collections.Generic;
using UnityEngine;


public class Deleter
{
	public static Deleter Instance;
	private int score = 0;
	private int count = 1;
	private int deleted = 0;
	private SweeperManager sweeper;
	private Grid theGrid;
	private List<Cluster> clustersToBeDeleted;

	public Deleter (Grid grid, SweeperManager sweep)
	{
		Instance = this;
		theGrid = grid;
		sweeper = sweep;
		clustersToBeDeleted = new List<Cluster> ();
	}

	public void Delete(int col){
		//Check all squares in col for cluster association and delete accordingly
		List<Square> squares = theGrid.GetSquaresInColumn (col);
		foreach (Square s in squares) {
			//Check if square has a cluster
			if(s.GetCluster() != null){
				if(s.GetCluster().GetRightBound() == s.GetGridColumn())
					if(!clustersToBeDeleted.Contains(s.GetCluster()))
						clustersToBeDeleted.Add (s.GetCluster ());
			}
		}
		DeleteClusters ();
	}
	
	public int GetScore() {
		return score;
	}
	
	public int GetDeleted() {
		return deleted;
	}

	private void DeleteClusters(){
		List<int> columns = new List<int> ();
		foreach (Cluster clust in clustersToBeDeleted) {
			foreach(Poly p in clust.GetPolyList()){
				score += (20 * count);
				count++;
				foreach(Square s in p.GetSquares()){
					if(s.GetBlock () == null){
						Debug.LogError ("UHhhh");
						continue;
					}
					s.GetBlock().RemoveSquare(s);
					columns.Add (s.GetGridColumn());
					theGrid.RemoveSquare(s);
					s.gameObject.SetActive (false);
					deleted++;
				}
			}
			count = 1;
		}
		clustersToBeDeleted.Clear ();
		ReDrop (columns);
	}

	public void ReDrop(List<int> columns){
		foreach (int i in columns) {
			foreach(Square s in theGrid.GetSquaresInColumn(i)){
				if(s != null){
					s.GetBlock().ReDrop();
				}
			}
		}
	}
}