using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		World code = (World)target;

		GUILayout.Space(5);
		if (GUILayout.Button("Make World", GUILayout.Height(30)))
		{
			code.MakeWorld();
		}
	}
}