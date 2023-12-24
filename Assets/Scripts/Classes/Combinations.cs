using UnityEngine;

public enum CombineResultType { Item, Ability, Wrong }

[System.Serializable]
public class CombineResults
{
	public string result;
	public CombineResultType type = CombineResultType.Item;
}

[System.Serializable]
public class CombineList
{
	public string[] combineRequirments;
	public CombineResults[] results;
	public bool wrong;
}

public class Combinations : MonoBehaviour
{
	public CombineList[] combineLists;

	public CombineList CheckoutCombination(string combo)
	{
		string[] combinedItems = combo.Split(' ');
		int matches = 0;
		for (int i = 0; i < combineLists.Length; i++)
		{
			for (int k = 0; k < combinedItems.Length; k++)
			{
				for (int j = 0; j < combineLists[i].combineRequirments.Length; j++)
					if (combinedItems[k] == combineLists[i].combineRequirments[j])
						matches++;

				if (matches == 0)
					break;

				if (matches >= combinedItems.Length)
				{
					return combineLists[i];
				}
			}
			matches = 0;
		}

		CombineList c = new CombineList();
		c.wrong = true;
		return c;
	}
}
