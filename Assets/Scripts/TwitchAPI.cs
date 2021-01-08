using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api.Models.Undocumented.Chatters;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchAPI : MonoBehaviour
{

    public Api api;
    Client getClient;
    TwitchClient twitchClient;
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        api = new Api();
        api.Settings.AccessToken = Secrets.BotAccessToken;
        api.Settings.ClientId = Secrets.ClientID;
        GameObject client = GameObject.Find("Client");
        twitchClient = client.GetComponent<TwitchClient>();
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
