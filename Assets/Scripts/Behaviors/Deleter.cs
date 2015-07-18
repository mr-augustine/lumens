using System;
using System.Collections.Generic;
using UnityEngine;


public class Deleter
{
	private List<Cluster> currentTurnClusters;
	private List<Cluster> nextTurnClusters;
	private SweeperManager sweeper;

	public Deleter ()
	{
		sweeper = GameObject.FindGameObjectWithTag ("TimeLine");
		sweeper.GetGridColumn ()
	}

	public void AddPoly(Poly p){

		//Does not overlap any clusters, create new cluster
		Cluster c = new Cluster (p);
		AddCluster (c);
	}

	public void AddCluster(Cluster c){
		//Which collection to add to?
		if (sweeper.GetGridColumn () < c.GetLeftBound ()) {
			//currentTurn
			AddToCurrentTurn (c);
		} else if (sweeper.GetGridColumn () > c.GetRightBound ()) {
			AddToNextTurn (c);
		} else {
			//die
		}
	}

	public void AddToCurrentTurn(Cluster c){
		foreach (Cluster curr in currentTurnClusters) {
			IsTouching(curr, c);
		}
	}

	public void AddToNextTurn(Cluster c){
		
	}

	//Should we join these two clusters into one cluster
	private Boolean IsTouching(Cluster current, Cluster newc){

	}
}