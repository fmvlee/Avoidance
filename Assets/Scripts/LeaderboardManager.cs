using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    const string LeaderboardId = "Avoidance";
    public bool scoreBool = false;
    public int newScore = 0;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    void Update()
    {
        if (!scoreBool)
        {
            AddScore(newScore);
            scoreBool = true;
        }
    }

    public async void AddScore(int score)
    {
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }
}
