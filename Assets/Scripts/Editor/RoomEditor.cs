using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
[CanEditMultipleObjects]
public class RoomEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Room code = (Room)target;

		GUILayout.Space(5);
		if (GUILayout.Button("Set Room Data", GUILayout.Height(30)))
		{
			code.SetRoomData();
		}
	}
}
