using System.Collections;
using UnityEngine;

public enum PreSpacing { Enter, Space }

[System.Serializable]
public class ConsoleWaitlist
{
    public string text;
    public float textTime;
    public bool isForcingToWrite;
    public bool isForcingToClearConsole;
    public PreSpacing ps;
    public bool isRunning;

    public string WriteText()
    {
        isRunning = true;

        string prefix = "";
        switch (ps)
        {
            case PreSpacing.Enter:
                prefix = "\n\n";
                break;
            case PreSpacing.Space:
                prefix = "\n";
                break;
        }

        return prefix + text;
    }
}

