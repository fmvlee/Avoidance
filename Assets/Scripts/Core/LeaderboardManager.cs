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
    public int[] globalTopTenHighScores;

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
        // Initialise 
        globalTopTenHighScores = new int[10];

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
        // Debug logs for testing
        //Debug.Log(JsonConvert.SerializeObject(playerEntry));
      //  Debug.Log($"Score: {playerEntry.Score}");
        //Debug.Log($"Player Name: {playerEntry.PlayerName}");
    }

    // Gets the current players high score
    public async void UpdatePlayerHiScore()
    {
        // Get the current players high score
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
        // Update highscore variable
        playerHighScore = Convert.ToInt32(scoreResponse.Score);
        // Debug logs for testing
        //Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    // Update global high score
    public async void UpdateGlobalHighScore()
    {   
        // Gets the highest score for the game
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 1 });
        // Update the highscore variable
        globalHighScore = Convert.ToInt32(scoresResponse.Results[0].Score);
        // Debug logs for testing
       // Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    // Update top 10 global high score
    public async void UpdateTopTenGlobalHighScore()
    {
        // Gets the highest score for the game
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 10 });

       // Debug.Log(JsonConvert.SerializeObject(scoresResponse.Results[0]));
        /*/ Update the highscore variable
        for(int i = 0; i < scoresResponse.Results.Count; i++)
        {
            globalTopTenHighScores[i] = Convert.ToInt32(scoresResponse.Results[i].Score);
        }*/

        foreach (var score in scoresResponse.Results)
        {
            //Debug.Log(score.PlayerName);
        }

        // Debug logs for testing
       // Debug.Log("Highscores: " + globalTopTenHighScores);
    }
}
