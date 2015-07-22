using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

	/// <summary>
	/// Changes to scene of the given index.
	/// </summary>
	/// <param name="sceneToChangeTo">Scene to change to.</param>
	public void ChangeToScene (int sceneToChangeTo)
	{
		Application.LoadLevelAsync (sceneToChangeTo);
	}

	/// <summary>
	/// Changes to scene of the given index. For programatic use.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public static void ChangeToSceneProg (int scene)
	{
		Application.LoadLevelAsync (scene);
	}
}
