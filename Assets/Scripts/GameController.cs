using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public World world;
	[SerializeField] private Player player;

	[Header("Console")]
	[SerializeField] private InputField console;
	[SerializeField] private List<ConsoleWaitlist> consoleWaitlist = new List<ConsoleWaitlist>();
	private float actionTimer;

	public Text commander_Text;

	private void Start()
	{
		world.rowRooms[(int)player.currentRoom.x].rooms[(int)player.currentRoom.y].InitializeRoom();
	}

	private void Update()
	{
		if (consoleWaitlist.Count > 0)
			CheckoutActions();
	}

	public void AddConsoleText(string textx, float textTimex, bool isForcingToWritex, bool isForcingToClearConsolex, PreSpacing preSpacex = PreSpacing.Enter)
	{
		var cw = new ConsoleWaitlist();
		cw.ps = preSpacex;
		cw.text = textx;
		cw.textTime = textTimex;
		cw.isForcingToWrite = isForcingToWritex;
		cw.isForcingToClearConsole = isForcingToClearConsolex;
		if (cw.isForcingToWrite)
			consoleWaitlist.Clear();

		consoleWaitlist.Add(cw);
	}

	private void CheckoutActions()
	{
		if (!consoleWaitlist[0].isRunning)
		{
			if (consoleWaitlist[0].isForcingToClearConsole)
				console.text = string.Empty;
			console.text += consoleWaitlist[0].WriteText();
		}

		actionTimer += Time.deltaTime;
		if (actionTimer >= consoleWaitlist[0].textTime)
		{
			consoleWaitlist.Remove(consoleWaitlist[0]);
			actionTimer = 0;
		}
	}

	public void AddCharacter(string character)
	{
		commander_Text.text += character;
	}

	public void BackSpace()
	{
		if (commander_Text.text.Length <= 0)
			return;

		var chars = commander_Text.text.ToCharArray(0, commander_Text.text.Length - 1);
		var temp = "";

		for (int i = 0; i < commander_Text.text.Length; i++)
		{
			if (i < commander_Text.text.Length - 1)
				temp += chars[i];
		}

		commander_Text.text = temp;
	}

	public void SubmitCommand()
	{
		CommandHandler.Command(commander_Text.text);
	}
}
