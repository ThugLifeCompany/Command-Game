using System.Collections.Generic;
using UnityEngine;

public class Requirment : MonoBehaviour
{
	[SerializeField] private List<string> requiredAbilitys;
	[SerializeField] private List<string> requiredItems;

	private Player player;

	private void Start()
	{
		GetComponent<Room>().entranceLock = true;
	}

	public string CheckoutRequirments()
	{
		player = GameObject.Find("Player").GetComponent<Player>();

		if (requiredItems.Count <= 0 && requiredItems.Count <= 0)
		{
			UnlockEntrance();
			return "success!";
		}

		bool reqiurmentCheck = false;
		string reqiurthings = string.Empty;
		for (int i = 0; i < requiredAbilitys.Count; i++)
		{
			if (!player.abilitys.Contains(requiredAbilitys[i]))
			{
				reqiurmentCheck = true;
				reqiurthings += requiredAbilitys[i] + "\n";
			}
		}

		if (!reqiurmentCheck)
		{
			UnlockEntrance();
			return "success!";
		}
		else
			return "you need this thing to unlock this move: \n" + reqiurthings;
	}

	private void UnlockEntrance()
	{
		GetComponent<Room>().entranceLock = false;
		Destroy(this);
	}
}
