using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgression
{
    public List<string> completedLevels;

    // Load back into Hub level, if all levels completed open boss room
    public void LevelComplete(string level)
    {
        if (!completedLevels.Contains(level))
        {
            completedLevels.Add(level);
        }
        if (completedLevels.Count == 3)
        {
            // Return to Hub level, open boss room
            // TO:DO Congrats Message
            SceneManager.LoadScene("Level00_HubBoss");
        }
        if (completedLevels.Count == 4)
        {
            // Player beat game, roll credits, return to main menu
            // TO:DO Congrats Message
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            // Return to Hub level
            // TO:DO Congrats Message
            SceneManager.LoadScene("Level00_Hub");
        }
    }

    // Load back into Hub level, if boss room is open keep open
    public void LevelDeath(string level)
    {
        if (completedLevels.Count == 3)
        {
            // Return to Hub Level, open boss room
            // TO:DO Death Message
            SceneManager.LoadScene("Level00_HubBoss");
        }
        else
        {
            // Return to Hub Level
            // TO:DO Death Message
            SceneManager.LoadScene("Level00_Hub");
        }
    }
}
