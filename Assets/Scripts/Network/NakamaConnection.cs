using System;
using Cysharp.Threading.Tasks;
using Nakama;
using UnityEngine;

namespace Network
{
    public class NakamaConnection : MonoBehaviour
    {
        // Configuration for local Docker setup
        private string scheme = "http";
        private string host = "127.0.0.1"; // Localhost
        private int port = 7350;           // The API port (not the console port)
        private string serverKey = "defaultkey"; // The key defined in docker-compose

        private IClient client;
        private ISession session;

        async void Start()
        {
            await AuthenticateUserAsync();
            await CreateGuildAsync();
        }
        
        private async UniTask CreateGuildAsync()
        {
            // 1. Define Group Details
            string groupName = "Warriors of Code";
            string description = "The official guild for this tutorial.";
            string avatarUrl = "http://example.com/icon.png";
            string langTag = "en"; // Language
            bool open = true; // True = Anyone can join. False = Request needed.
            int maxCount = 50; // Max members

            try 
            {
                // 2. Send request to Server
                // Nakama returns a generic IGroup interface
                var group = await client.CreateGroupAsync(session, groupName, description, avatarUrl, langTag, open, maxCount);
            
                Debug.Log("Group Created Successfully!");
                Debug.Log("Group ID: " + group.Id);
                Debug.Log("Group Name: " + group.Name);
            }
            catch (ApiResponseException ex) 
            {
                // If you run this twice, it might fail because the group name implies uniqueness depending on configuration,
                // or if you simply spam creation.
                Debug.LogError("Could not create group: " + ex.Message);
            }
        }
        private async UniTask AuthenticateUserAsync()
        {
            client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
            var deviceId = SystemInfo.deviceUniqueIdentifier;
            try
            {
                session = await client.AuthenticateDeviceAsync(deviceId, username: null, create: true);
                Debug.Log("Successfully authenticated!");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error authenticating: " + ex.Message);
            }
        }
    }
}