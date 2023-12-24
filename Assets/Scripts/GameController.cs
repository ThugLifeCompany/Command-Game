using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public World world;
	public Room currentRoom;
	[SerializeField] private Player player;

	[Header("Console")]
	[SerializeField] private InputField console;
	[SerializeField] private List<ConsoleWaitlist> consoleWaitlist = new List<ConsoleWaitlist>();
	private float actionTimer;

	[SerializeField] private Text commander_Text;
	[SerializeField] private Text status_Text;

	private void Start()
	{
		CommandHandler.player = GameObject.Find("Player").GetComponent<Player>();
		CommandHandler.gc = GameObject.Find("GameController").GetComponent<GameController>();
		CommandHandler.world = GameObject.Find("World").GetComponent<World>();
		CommandHandler.combinations = GameObject.Find("Combinations").GetComponent<Combinations>();
		MoveToRoom(Vector2.zero);
	}

	private void Update()
	{
		if (consoleWaitlist.Count > 0)
			CheckoutActions();
	}

	/// <summary>
	/// Move to the Room you want
	/// </summary>
	/// <param name="offsetCoordinate">The offset direction of coordinate.</param>
	public void MoveToRoom(Vector2 offsetCoordinate)
	{
		var playerDestination = world.rowRooms[(int)player.currentRoom.x + (int)offsetCoordinate.x].rooms[(int)player.currentRoom.y + (int)offsetCoordinate.y];
		string entrance = string.Empty;
		if (playerDestination.gameObject.GetComponent<Requirment>() != null)
			entrance = playerDestination.gameObject.GetComponent<Requirment>().CheckoutRequirments();
		else
			playerDestination.InitializeRoom();

		if (entrance == "success")
			playerDestination.InitializeRoom();
		else
			AddConsoleText(entrance, 0, false, false, PreSpacing.Enter);
	}

	//Console Codes -----------------------------------------------------------
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

	public void UpdateConsole()
	{
		commander_Text.text = string.Empty;
		status_Text.text = currentRoom.GetStatus();
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
