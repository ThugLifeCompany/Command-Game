using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public string roomName;
	public string property;
	public Vector2 roomID;
	public string availableMoves = "north east south west";
	public bool entranceLock;
	public List<string> objects;

	public void SetRoomData()
	{
		var textObj = transform.GetChild(0).gameObject;
		textObj.GetComponent<TextMeshPro>().text = roomName;
	}

	public void InitializeRoom()
	{
		CommandHandler.gc.currentRoom = this;
		CommandHandler.player.currentRoom = roomID;
		CommandHandler.gc.UpdateConsole();
		CommandHandler.gc.AddConsoleText("you moved!, items and moves are updated!", 1, false, false, PreSpacing.Enter);
	}

	public string GetStatus()
	{
		var text = string.Format("you are in {0} {1}\nobjects: ", property, roomName);
		for (int i = 0; i < objects.Count; i++)
		{
			if (i < objects.Count - 1)
				text += objects[i] + " - ";
			else
				text += objects[i] + "\n";
		}

		text += "available moves : " + availableMoves + "\n";
		return "commands : grab - move - hit - kill - use - search - open - combine - drop - update\n\n" + text;
	}
}