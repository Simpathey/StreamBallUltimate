﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Secrets
{
    public static string client_id;
    public static string client_secret; 
    public static string bot_access_token; 
    public static string bot_refresh_token; //hai
    public static void EnvironmentVariables()
    {
        client_id = GetVariable("client_id");
        client_secret = GetVariable("client_secret");
        bot_access_token = GetVariable("bot_access_token");
        bot_refresh_token = GetVariable("bot_refresh_token");
        /*
        string value;
        bool toDelete = false;

        // Check whether the environment variable exists.
        value = Environment.GetEnvironmentVariable("client_id");
        // If necessary, create it.
        if (value == null)
        {
            Environment.SetEnvironmentVariable("client_id", "");
            Environment.SetEnvironmentVariable("client_secret", "");
            Environment.SetEnvironmentVariable("bot_access_token", "");
            Environment.SetEnvironmentVariable("bot_refresh_token", "");
            toDelete = true;

            // Now retrieve it.
            client_id = Environment.GetEnvironmentVariable("client_id");
            client_secret = Environment.GetEnvironmentVariable("client_secret");
            bot_access_token = Environment.GetEnvironmentVariable("bot_access_token");
            bot_refresh_token = Environment.GetEnvironmentVariable("bot_refresh_token");
   */
    }
    
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
