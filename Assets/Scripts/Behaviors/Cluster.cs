using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster
{
	//private int LeftBound;
	//private int RightBound;
	private int PolyCount;
	private List<Poly> PolyList;
	// I don't do anything yet.

	public Cluster(Poly p){
		PolyCount = 1;
		PolyList = new List<Poly>();
		PolyList.Add (p);
		//LeftBound = p.GetLeftBound ();
		//RightBound = p.GetRightBound ();
	}

	public void AddPoly(Poly p){
		foreach(Poly poly in PolyList){
			if(!poly.GetID().Equals(p.GetID())){
				PolyList.Add (p);
				PolyCount++;
				break;
			}
		}
	}

	public List<Poly> GetPolyList() {
		return PolyList;
	}
}

