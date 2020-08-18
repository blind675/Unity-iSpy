using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour {
	public void LoadGameScene ()
	{
		PlayerInvisibility.IsPlayerInvisible = false;
		SceneManager.LoadScene (sceneName: "SampleScene");
	}

	public static void LoadGameOverScene ()
	{
		SceneManager.LoadScene (sceneName: "GameOverScene");
	}
}
