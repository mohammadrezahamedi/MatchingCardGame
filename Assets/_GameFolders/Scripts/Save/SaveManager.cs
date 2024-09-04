using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour
{
    private const string FileName = "game_data.json";
    private GameData gameData;

    private void OnEnable()
    {
        UIManager.OnResetGame += SaveScore;
        LoadScore(); 
    }

    private void OnDisable()
    {
        UIManager.OnResetGame -= SaveScore;
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, FileName);
    }

    private void SaveScore(int totalScore)
    {
        string currentLevelID = GetCurrentLevelID();

        if (gameData == null)
        {
            gameData = new GameData();
        }

        // Update current score
        gameData.currentScore = totalScore;

        if (gameData.highestScores.ContainsKey(currentLevelID))
        {
            if (totalScore > gameData.highestScores[currentLevelID])
            {
                gameData.highestScores[currentLevelID] = totalScore;
                Debug.Log("New highest score for level " + currentLevelID + ": " + totalScore);
            }
        }
        else
        {
            gameData.highestScores[currentLevelID] = totalScore;
            Debug.Log("Setting highest score for level " + currentLevelID + ": " + totalScore);
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(GetFilePath(), json);

        Debug.Log("Game data saved.");
    }
    private void LoadScore()
    {
        string filePath = GetFilePath();
        string currentLevelID = GetCurrentLevelID();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            gameData = JsonUtility.FromJson<GameData>(json);
            if (gameData.highestScores.ContainsKey(currentLevelID))
            {
                Debug.Log("Highest score for level " + currentLevelID + ": " + gameData.highestScores[currentLevelID]);
            }
            else
            {
                Debug.Log("No highest score found for level " + currentLevelID);
            }
        }
        else
        {
            gameData = new GameData();
            Debug.Log("No saved data found.");
        }
    }

    public int GetHighestScore()
    {
        string currentLevelID = GetCurrentLevelID();
        if (gameData != null && gameData.highestScores.ContainsKey(currentLevelID))
        {
            return gameData.highestScores[currentLevelID];
        }
        return 0; // Return 0 if no highest score exists for this level
    }

    private string GetCurrentLevelID()
    {
        return "Level_1";
    }
}

[Serializable]
public class GameData
{
    public int currentScore;
    public Dictionary<string, int> highestScores = new Dictionary<string, int>(); 
}
