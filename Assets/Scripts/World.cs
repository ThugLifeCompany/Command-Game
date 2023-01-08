using UnityEngine;

[System.Serializable]
public class Rooms { public Room[] rooms; }

public class World : MonoBehaviour
{
	public Rooms[] rowRooms;
	[SerializeField] private GameObject roomPrefab;
	[SerializeField] private int roomsCountsInRows;
	[SerializeField] private float roomDistance;

	public void MakeWorld()
	{
		for (int i = 0; i < rowRooms.Length; i++)
			rowRooms[i].rooms = new Room[roomsCountsInRows];

		for (int i = 0; i < rowRooms.Length; i++)
		{
			var rowObj = new GameObject("Row_" + i);
			rowObj.transform.SetParent(transform.GetChild(0));
			for (int j = 0; j < roomsCountsInRows; j++)
			{
				var roomObj = Instantiate(roomPrefab, new Vector2(i * roomDistance, j * -roomDistance), Quaternion.identity);
				roomObj.name = "Room_" + i + "_" + j;
				roomObj.transform.SetParent(rowObj.transform);
				var room = roomObj.GetComponent<Room>();
				rowRooms[i].rooms[j] = room;
				room.roomID = new Vector2(i, j);
			}
		}
	}
}
