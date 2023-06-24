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

public class LeaderboardManager : MonoBehaviour
{
    const string leaderboardId = "Avoidance";
    public bool scoreBool = false;
    public int newScore = 0;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    /*void Update()
    {
        if (!scoreBool)
        {
            AddScore(newScore);
            scoreBool = true;
        }
    }*/

    public async void AddScore(int score)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    public async void GetPlayerScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetHighScore()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async Task<LeaderboardScoresPage> GetHighScoreVTwo()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        return scoresResponse;
    }
}
