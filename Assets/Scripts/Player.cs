using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public Vector2 currentRoom;
	public List<string> abilitys;

	[Header("Backpack")]
	public List<string> backpack;
	public int backpackSlots;

	[Header("Note")]
	[SerializeField, TextArea] private string note;
}
