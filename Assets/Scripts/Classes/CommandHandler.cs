using UnityEngine;

public static class CommandHandler
{
	[Header("Needs")]
	private static Player player;
	private static GameController gc;
	private static World world;

	[Header("Lists")]
	public static string[] grabbableItems = { "candle", "lighter", "backpack" };

	public static void Command(string command)
	{
		var cs = command.Split(' ');

		if (player == null || gc == null || world == null)
		{
			player = GameObject.Find("Player").GetComponent<Player>();
			gc = GameObject.Find("GameController").GetComponent<GameController>();
			world = GameObject.Find("World").GetComponent<World>();
		}

		switch (cs[0])
		{
			case "move":
				break;
			case "grab":
				Grab(cs);
				break;
			case "search":
				break;
			case "hit":
				break;
			case "open":
				break;
			case "use":
				break;
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
			world.rowRooms[(int)player.currentRoom.x].rooms[(int)player.currentRoom.y].GetComponent<Room>().objects.Remove(cs[1]);
			gc.AddConsoleText("now you have a backpack with " + player.backpackSlots + " slots", 0, false, false, PreSpacing.Enter);
			return;
		}

		if (player.backpackSlots <= 0)
		{
			gc.AddConsoleText("you dont have a backpack!", 0, false, false, PreSpacing.Enter);
			return;
		}

		if (isGrabbable(cs[1]))
		{
			if (player.backpack.Count >= player.backpackSlots)
			{
				gc.AddConsoleText("backpack is full", 0, false, false, PreSpacing.Enter);
				return;
			}

			gc.AddConsoleText("now you have a \"" + cs[1] + "\" in your backpack", 0, false, false, PreSpacing.Enter);
			player.backpack.Add(cs[1]);
			world.rowRooms[(int)player.currentRoom.x].rooms[(int)player.currentRoom.y].GetComponent<Room>().objects.Remove(cs[1]);
			gc.commander_Text.text = string.Empty;
		}
		else
			gc.AddConsoleText("something went wrong!", 0, false, false, PreSpacing.Enter);
	}

	private static bool isGrabbable(string item)
	{
		for (int i = 0; i < grabbableItems.Length; i++)
		{
			if (item == grabbableItems[i])
				return true;
		}
		return false;
	}
}
