using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
	public string roomName;
	public string property;
	public Vector2 roomID;
	public bool entranceLock;
	public List<string> objects;

	public void SetRoomData()
	{
		var textObj = transform.GetChild(0).gameObject;
		textObj.GetComponent<TextMeshPro>().text = roomName;
	}

	public void InitializeRoom()
	{
		var text = string.Format("you are in {0} {1}\nobjects: ", property, roomName);
		for (int i = 0; i < objects.Count; i++)
		{
			if (i < objects.Count - 1)
				text += objects[i] + " - ";
			else
				text += objects[i] + "\n";
		}

		GameObject.Find("GameController").GetComponent<GameController>().AddConsoleText(text, 0, false, true, PreSpacing.None);
	}
}
