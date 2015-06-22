using UnityEngine;
using System.Collections;

public enum SquareType
{
	WHITE,
	ORANG,
	SPC_W,
	SPC_O}
;

public class Square : MonoBehaviour
{
	[SerializeField]
	private SquareType
		type;

	void Start ()
	{
		Material mat = GetComponent<Renderer> ().material;
		switch (type) {
		case SquareType.WHITE:
			mat.color = Color.white;
			break;
		case SquareType.SPC_W:
			mat.color = Color.white;
			break;
		case SquareType.ORANG:
			mat.color = Color.red;
			break;
		case SquareType.SPC_O:
			mat.color = Color.red;
			break;
		}
	}

	public SquareType GetType ()
	{
		return type;
	}
}
