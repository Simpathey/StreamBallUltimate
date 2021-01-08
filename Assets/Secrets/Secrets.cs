using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Secrets
{
    public static string ClientID { get; } = Environment.GetEnvironmentVariable("client_id");
    public static string ClientSecret { get; } = Environment.GetEnvironmentVariable("client_secret");
    public static string BotAccessToken { get; } = Environment.GetEnvironmentVariable("bot_access_token");
    public static string BotRefreshToken { get; } = Environment.GetEnvironmentVariable("bot_refresh_token"); //hai

}
