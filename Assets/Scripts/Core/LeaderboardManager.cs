using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;
using Unity.Services.Leaderboards.Models;
using Newtonsoft.Json.Linq;
using System;

// Responsible for handling the Unit Leaderboard Service
public class LeaderboardManager : MonoBehaviour
{
    // Leaderboard ID from Unity Service
    const string leaderboardId = "Avoidance";

    // Variables to store scores
    public int playerHighScore = 0;
    public int globalHighScore = 0;
    public List<int> globalTopTenHighScores;

    // Called before any other methods
    private async void Awake()
    {
        // Initialise service
        await UnityServices.InitializeAsync();
        // Check the user is signed in, if not then sign in
        if (!AuthenticationService.Instance.IsSignedIn)
        {            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        // Update the scores
        UpdatePlayerHiScore();
        UpdateGlobalHighScore();
        UpdateTopTenGlobalHighScore();
    }

    // Adds a score to the unity service
    public async void AddScore(int score)
    {
        // Add score to leaderboard
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        // Update the global high scores
        UpdateTopTenGlobalHighScore();
        // Debug logs for testing
        //Debug.Log(JsonConvert.SerializeObject(playerEntry));
        //Debug.Log($"Score: {playerEntry.Score}");
        //Debug.Log($"Player Name: {playerEntry.PlayerName}");
    }

    // Gets the current players high score
    public async void UpdatePlayerHiScore()
    {
        // Get the current players high score
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
        // Update highscore variable
        if (scoreResponse.PlayerId != null)
        {
            playerHighScore = Convert.ToInt32(scoreResponse.Score);
        }
    }

    // Update global high score
    public async void UpdateGlobalHighScore()
    {   
        // Gets the highest score for the game
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 1 });
        // Update the highscore variable
        if (scoresResponse.Results.Count > 0)
        {
            globalHighScore = Convert.ToInt32(scoresResponse.Results[0].Score);
        }
        // Debug logs for testing
        //Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    // Update top 10 global high score
    public async void UpdateTopTenGlobalHighScore()
    {
        // Ensure the list is empty
        globalTopTenHighScores.Clear();
        // Gets the highest score for the game
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 10 });
        // Initialise 
        // Update the highscore variable
        if (scoresResponse.Results.Count > 0)
        {
            for (int i = 0; i < scoresResponse.Results.Count; i++)
            {
                globalTopTenHighScores.Add(Convert.ToInt32(scoresResponse.Results[i].Score));
                Debug.Log(Convert.ToInt32(scoresResponse.Results[i].Score));
            }
        }
    }
}
