using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Zenject;

namespace MatchingCards
{
    public class SaveManager : MonoBehaviour
    {
        private const string FileName = "game_data.json";
        private GameData gameData;
        private UIManager _uIManager;

        [Inject]
        public void Constructor(UIManager uiManager)
        {
            _uIManager = uiManager;
        }

        private void Awake()
        {
            LoadScore();
        }

     
        private string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, FileName);
        }

        public void SaveScore(int totalScore)
        {
            string currentLevelID = GetCurrentLevelID();

            if (gameData == null)
            {
                gameData = new GameData();
            }

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

            _uIManager.UpdateHighestScore(gameData.highestScores[currentLevelID]);

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

                    _uIManager.UpdateHighestScore(gameData.highestScores[currentLevelID]);
                }
                else
                {
                    _uIManager.UpdateHighestScore(0);  // Default to 0 
                }
            }
            else
            {
                gameData = new GameData();
                Debug.Log("No saved data found.");
                _uIManager.UpdateHighestScore(0);
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
            return "Level_1";  // Replace this with your logic for determining the current level
        }
    }

    [Serializable]
    public class GameData
    {
        public int currentScore;
        public Dictionary<string, int> highestScores = new Dictionary<string, int>();
    }
}
