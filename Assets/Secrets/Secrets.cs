using System;
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
        client_id = Environment.GetEnvironmentVariable("client_id");
        client_secret = Environment.GetEnvironmentVariable("client_secret");
        bot_access_token = Environment.GetEnvironmentVariable("bot_access_token");
        bot_refresh_token = Environment.GetEnvironmentVariable("bot_refresh_token");
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
}
