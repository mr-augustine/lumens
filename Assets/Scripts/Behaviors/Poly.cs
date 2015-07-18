using UnityEngine;
using System.Collections;
using System.Collections.Generic; //so we can use List<T>

// Just making this a good old-fashioned class; no Mono req'd
using System.Text;


public class Poly //: MonoBehaviour
{
	private List<Square> squares;
	private string ID;

	public Poly(Square first, Square second, Square third, Square fourth) {
		squares = new List<Square>();

		//right square
		squares.Add(first);
		//left squares
		squares.Add(second);
		squares.Add(third);
		//right square
		squares.Add(fourth);

		//Create Poly ID
		StringBuilder sb = new StringBuilder ();
		sb.Append (first.GetGridRow ().ToString ());
		sb.Append (first.GetGridColumn ().ToString ());
		sb.Append (second.GetGridRow ().ToString ());
		sb.Append (second.GetGridColumn ().ToString ());
		sb.Append (third.GetGridRow ().ToString ());
		sb.Append (third.GetGridColumn ().ToString ());
		sb.Append (fourth.GetGridRow ().ToString ());
		sb.Append (fourth.GetGridColumn ().ToString ());
		ID = sb.ToString ();
	}

	public List<Square> GetSquares() {
		return squares;
	}

	// Two Polys are consider equal if they contain the same Squares
	public bool Equals(Poly other) {
		if (other == null) {
			return false;
		} else {
			bool allSquaresShared = true;

			foreach (Square square in other.GetSquares ()) {
				allSquaresShared &= squares.Contains(square);
			}

			return allSquaresShared;
		}
	}

	public string GetID(){
		return ID;
	}

	public int GetLeftBound() {
		return squares[1].GetGridColumn();
	}

	public int GetRightBound() {
		return squares[0].GetGridColumn();
	}

	public void UpdateClusterRef(Cluster curr) {
		foreach (Square square in squares) {
			square.SetCluster(curr);
		}
	}

	/*// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}*/
}

