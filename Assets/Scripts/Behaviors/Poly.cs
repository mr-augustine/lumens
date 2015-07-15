using UnityEngine;
using System.Collections;
using System.Collections.Generic; //so we can use List<T>

// Just making this a good old-fashioned class; no Mono req'd
public class Poly //: MonoBehaviour
{
	private List<Square> squares;

	public Poly(Square first, Square second, Square third, Square fourth) {
		squares = new List<Square>();

		squares.Add(first);
		squares.Add (second);
		squares.Add(third);
		squares.Add (fourth);
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




	/*// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}*/
}

