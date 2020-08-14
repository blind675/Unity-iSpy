using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static bool mainActionEngaged = false;

	public void MainButtonPressed ()
	{
		mainActionEngaged = true;
	}

	public void MainButtonReleased ()
	{
		mainActionEngaged = false;
	}

}
