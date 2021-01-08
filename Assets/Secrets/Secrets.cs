using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Secrets
{
    public static string ClientID { get; } = GetVariable("client_id");
    public static string ClientSecret { get; } = GetVariable("client_secret");
    public static string BotAccessToken { get; } = GetVariable("bot_access_token");
    public static string BotRefreshToken { get; } = GetVariable("bot_refresh_token"); //hai

    /// <summary>Gets the environment variable at any target level.</summary>
    /// <remarks>Attempts to get the environment variable in the order process-wide, user-wide, and finally machine-wide.</remarks>
    /// <param name="name">The name of the environment variable</param>
    /// <returns>Returns the value of environment variable or null if not found at any level</returns>
    public static string GetVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ??
               Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User) ??
               Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
    }

}
