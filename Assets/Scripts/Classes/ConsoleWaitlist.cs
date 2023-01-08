using UnityEngine;

public enum PreSpacing { None, Enter, Tab, Space}

[System.Serializable]
public class ConsoleWaitlist
{
	[TextArea] public string text;
	public float textTime;
	public bool isRunning;
	public PreSpacing ps = PreSpacing.Enter;
	public bool isForcingToClearConsole;
	public bool isForcingToWrite;

	public string WriteText()
	{
		isRunning = true;

		switch (ps)
		{
			case PreSpacing.Enter:
				return "\n" + text;
			case PreSpacing.Tab:
				return "\t" + text;
			case PreSpacing.Space:
				return " " + text;
			default:
				return text;

		}
	}
}
