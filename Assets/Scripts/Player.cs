using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxMana = 100f;
	public ManaBar manaBar;

	private float mana;

	// Start is called before the first frame update
	void Start ()
	{
		mana = maxMana;
		manaBar.SetMaxMana (mana);
	}

	public bool CanConsumeManaAmount (float manaAmountToUse)
	{
		return mana - manaAmountToUse > 0;
	}

	public bool CanRestoreMana ()
	{
		return !(mana == maxMana);
	}

	public void UseMana (float manaAmounToUse)
	{
		if (CanConsumeManaAmount (manaAmounToUse)) {
			mana -= manaAmounToUse;

			manaBar.SetMana (mana);
		} else {
			Debug.LogError ("Not Enough Mana");
		}
	}

	public void RestoreMana (float manaAmountToRestore)
	{
		mana += manaAmountToRestore;

		if (mana > maxMana) {
			mana = maxMana;
		}

		manaBar.SetMana (mana);
	}
}
