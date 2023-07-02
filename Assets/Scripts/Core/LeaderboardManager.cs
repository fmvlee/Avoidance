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

public class LeaderboardManager : MonoBehaviour
{
    const string leaderboardId = "Avoidance";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async void AddScore(int score)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
        Debug.Log($"Score: {playerEntry.Score}");
        Debug.Log($"Player Name: {playerEntry.PlayerName}");
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
}
