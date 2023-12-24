using System.Collections.Generic;
using UnityEngine;

public class Requirment : MonoBehaviour
{
	[SerializeField] private List<string> requiredAbilitys;
	[SerializeField] private List<string> requiredItems;

	private void Start()
	{
		GetComponent<Room>().entranceLock = true;
	}

	public string CheckoutRequirments()
	{
		if (requiredAbilitys.Count <= 0 && requiredItems.Count <= 0)
		{
			UnlockEntrance();
			return "success";
		}

		bool reqiurmentCheck = false;
		string reqAbility = string.Empty;
		for (int i = 0; i < requiredAbilitys.Count; i++)
		{
			if (!CommandHandler.player.abilitys.Contains(requiredAbilitys[i]))
			{
				reqiurmentCheck = true;
				reqAbility += requiredAbilitys[i] + "\t";
			}
		}

		string reqItems = string.Empty;
		for (int i = 0; i < requiredItems.Count; i++)
		{
			if (!CommandHandler.player.abilitys.Contains(requiredAbilitys[i]))
			{
				reqiurmentCheck = true;
				reqItems += requiredAbilitys[i] + "\t";
			}
		}

		if (!reqiurmentCheck)
		{
			UnlockEntrance();
			return "success";
		}
		else
			return "you need this things to unlock this move: \n" + reqAbility + requiredItems;
	}

	private void UnlockEntrance()
	{
		GetComponent<Room>().entranceLock = false;
		Destroy(this);
	}
}
