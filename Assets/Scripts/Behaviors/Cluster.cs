using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster
{
	private int RightBound;
	private int PolyCount;
	private List<Poly> PolyList;
	// I don't do anything yet.

	public Cluster(Poly p){
		RightBound = -1;
		PolyCount = 1;
		PolyList = new List<Poly>();
		PolyList.Add (p);
		UpdateRightBound();
	}

	/// <summary>
	/// Adds the specified Poly to the Cluster.
	/// </summary>
	/// <param name="p">The Poly.</param>
	public void AddPoly(Poly p){
		foreach(Poly poly in PolyList) {

			// Only add the Poly if it doesn't already exist in the Cluster
			if(!poly.GetID().Equals(p.GetID())) {
				PolyList.Add (p);
				UpdateRightBound();
				PolyCount++;
				break;
			}
		}
	}

	public List<Poly> GetPolyList() {
		return PolyList;
	}

	public void UpdateRightBound(){
		foreach (Poly p in PolyList) {
			foreach(Square s in p.GetSquares ()){
				if(s.GetGridColumn() > RightBound)
					RightBound = s.GetGridColumn();
			}
		}
	}

	public int GetRightBound(){
		return RightBound;
	}
}

