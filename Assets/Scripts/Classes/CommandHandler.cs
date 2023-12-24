using UnityEngine;

public static class CommandHandler
{
	[Header("Needs")]
	public static Player player;
	public static GameController gc;
	public static World world;
	public static Combinations combinations;

	[Header("Lists")]
	public static string[] grabbableItems = { "candle", "lighter", "backpack" };
	public static string[] hideablePlaces = { "Bush", "Closet" };

	public static void Command(string command)
	{
		var cs = command.Split(' ');
		switch (cs[0])
		{
			case "move": Move(cs[1]); break;
			case "grab": Grab(cs); break;
			case "search": break;
			case "hit": break;
			case "open": break;
			case "combine": Combine(cs); break;
			case "drop": Drop(cs[1]); break;
			default: gc.AddConsoleText("wrong command!", 0, false, false, PreSpacing.Enter); break;
		}
	}

	private static void Grab(string[] cs)
	{
		if (cs[1] == "backpack")
		{
			if (player.backpackSlots <= 0)
			{
				player.backpackSlots += 10;
				player.abilitys.Add(cs[1]);
			}
			else
				player.backpackSlots += 2;
			gc.currentRoom.objects.Remove(cs[1]);
			gc.AddConsoleText("now you have a backpack with " + player.backpackSlots + " slots", 0, false, false, PreSpacing.Enter);
			gc.UpdateConsole();
			return;
		}

		if (player.backpackSlots <= 0)
		{
			gc.AddConsoleText("you dont have a backpack!", 0, false, false, PreSpacing.Enter);
			return;
		}

		if (!gc.currentRoom.objects.Contains(cs[1]))
		{
			gc.AddConsoleText("this object does not exist in this room!", 0, false, false, PreSpacing.Enter);
			return;
		}

		if (isAvailable(cs[1], grabbableItems))
		{
			if (player.backpack.Count >= player.backpackSlots)
			{
				gc.AddConsoleText("backpack is full", 0, false, false, PreSpacing.Enter);
				return;
			}

			gc.AddConsoleText("now you have a \"" + cs[1] + "\" in your backpack", 0, false, false, PreSpacing.Enter);
			player.backpack.Add(cs[1]);
			gc.currentRoom.objects.Remove(cs[1]);
			gc.UpdateConsole();
		}
		else
			gc.AddConsoleText("something went wrong!", 0, false, false, PreSpacing.Enter);
	}

	private static bool isAvailable(string item, string[] list)
	{
		for (int i = 0; i < list.Length; i++)
		{
			if (item == list[i])
				return true;
		}
		return false;
	}

	private static void Drop(string item)
	{
		if (player.backpack.Contains(item))
		{
			gc.currentRoom.objects.Add(item);
			player.backpack.Remove(item);
			gc.AddConsoleText("you dropped \"" + item + "\"", 0, false, false, PreSpacing.Enter);
			gc.UpdateConsole();
		}
		else
			gc.AddConsoleText("you dont have \"" + item + "\" in your backpack", 0, false, false, PreSpacing.Enter);
	}

	private static void Combine(string[] rawItems)
	{
		var combo = string.Empty;

		for (int i = 1; i < rawItems.Length; i++)
		{
			combo += rawItems[i];
			if (i < rawItems.Length - 1)
				combo += " ";
		}

		CombineList result = combinations.CheckoutCombination(combo);
		//Thread.Sleep(3000);
		if (result.wrong)
		{
			gc.AddConsoleText("cannot combine this!", 0, false, false, PreSpacing.Enter);
			return;
		}

		for (int i = 1; i < rawItems.Length; i++)
		{
			player.backpack.Remove(rawItems[i]);
			gc.AddConsoleText("item \"" + rawItems[i] + "\" removed", 0.4f, false, false, PreSpacing.Enter);
		}

		string resultText = "now you have : ";
		for (int i = 0; i < result.results.Length; i++)
		{
			switch (result.results[i].type)
			{
				case CombineResultType.Ability: player.abilitys.Add(result.results[i].result); break;
				case CombineResultType.Item: player.backpack.Add(result.results[i].result); break;
			}
			resultText += "\"" + result.results[i].result + "\" ";
		}

		gc.AddConsoleText(resultText, 0, false, false, PreSpacing.Enter);
		gc.UpdateConsole();
	}

	private static void Move(string direction)
	{
		bool wDir = false;
		bool wHide = false;
		if (isAvailable(direction, gc.currentRoom.availableMoves.Split(' ')))
		{
			Vector2 coordinate = DirectionToCoordinate(direction);
			gc.MoveToRoom(coordinate);
			gc.UpdateConsole();
			return;
		}

		if (isAvailable(direction, hideablePlaces))
		{
			Hide();
			gc.UpdateConsole();
			return;
		}

		if (!wDir && !wHide)
			gc.AddConsoleText("wrong direction input\ndirections are : " + gc.currentRoom.availableMoves + "\n\tor a hide place", 0, false, false, PreSpacing.Enter);
	}

	private static Vector2 DirectionToCoordinate(string direction)
	{
		Vector2 dir = Vector2.zero;
		switch (direction)
		{
			case "north": dir = Vector2.up; break;
			case "east": dir = Vector2.right; break;
			case "west": dir = Vector2.left; break;
			case "south": dir = Vector2.down; break;
		}
		return dir;
	}

	private static void Hide()
	{
		gc.AddConsoleText("You are hided!", 0, false, false, PreSpacing.Enter);
	}
}

