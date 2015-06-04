using UnityEngine;
using System.Collections;

/// <summary>
/// Documentation example.
/// 
/// This is an example of how you should document your code for this project.
/// </summary>
public class DocumentationExample : MonoBehaviour
{
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		ExampleMethod (2, "Please document your code!");
	}

	/// <summary>
	/// An example method that logs its two given variables to the debugging console.
	/// </summary>
	/// <param name="exVar1">Example Variable 1.</param>
	/// <param name="exVar2">Example Variable 2.</param>
	public void ExampleMethod (int exVar1, string exVar2)
	{
		exVar1++;
		Debug.Log (exVar2 + exVar1.ToString ());
	}
}
