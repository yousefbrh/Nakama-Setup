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
            // 1. Create the Client
            // The client represents the connection configuration to the server.
            client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);

            // 2. Authenticate
            // We will use the device's unique ID.
            // If the account exists, it logs in. If not, it creates a new one.
            string deviceId = SystemInfo.deviceUniqueIdentifier;
        
            try
            {
                // The 'true' argument tells Nakama to create the account if it doesn't exist.
                session = await client.AuthenticateDeviceAsync(deviceId, username: null, create: true);

                Debug.Log("Successfully authenticated!");
                Debug.Log("Session Token: " + session.AuthToken);
                Debug.Log("User ID: " + session.UserId);
            }
            catch (ApiResponseException ex)
            {
                Debug.LogError("Error authenticating: " + ex.Message);
            }
        }
    }
}