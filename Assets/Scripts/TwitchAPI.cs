using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api.Models.Undocumented.Chatters;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchAPI : MonoBehaviour
{

    public Api API;
    Client GetClient;
    TwitchClient TwitchClient;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        API = new Api();
        API.Settings.AccessToken = Secrets.BotAccessToken;
        API.Settings.ClientId = Secrets.ClientID;
        GameObject client = GameObject.Find("Client");
        TwitchClient = client.GetComponent<TwitchClient>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            api.Invoke(api.Undocumented.GetChattersAsync
                (twitchClient.client.JoinedChannels[0].Channel), GetChattersListCallBack);
        }
        */
    }

    private void GetChattersListCallBack(List<ChatterFormatted> listOfChatters)
    {
        Debug.Log("List of " + listOfChatters.Count + "Viewers: ");
        foreach (var chatterObject in listOfChatters)
        {
            // Debug.Log(chatterObject.Username);
        }
    }
}
