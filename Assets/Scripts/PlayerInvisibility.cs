using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibility : MonoBehaviour {

	public float ManaAmountConsumtionPerSecond = 20;
	public float ManaAmountRestoredPerSecond = 10;

	public static bool IsPlayerInvisible { get; set; } = false;

	private Player player;

	private void Start ()
	{
		player = GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update ()
	{
		float manaAmountToConsume = ManaAmountConsumtionPerSecond * Time.deltaTime;

		//if (Input.GetKeyDown (KeyCode.H)) {
		//	if (isPlayerInvisible) {
		//		isPlayerInvisible = false;
		//	} else {
		//		isPlayerInvisible = true;
		//	}
		//} else {
		//	isPlayerInvisible = false;
		//}

		if (InputManager.mainActionEngaged) {
			IsPlayerInvisible = true;
		} else {
			IsPlayerInvisible = false;
		}

		if (IsPlayerInvisible) {
			// use mana

			if (player.CanConsumeManaAmount (manaAmountToConsume)) {
				player.UseMana (manaAmountToConsume);
			} else {
				IsPlayerInvisible = false;
			}

		} else {
			// restore mana

			if (player.CanRestoreMana ()) {
				float manaAmountToRestore = ManaAmountRestoredPerSecond * Time.deltaTime;
				player.RestoreMana (manaAmountToRestore);
			}
		}
	}


}
