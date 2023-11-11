using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using Riptide.Utils;
using System;

// Written inspired by Riptide Tutorial (Assignment Reference)
// https://www.youtube.com/watch?v=6kWNZOFcFQw
public class NetworkManager : MonoBehaviour
{
    // Defining the message type transferring from client to server
    public enum MessageType : ushort
    {
        // Send user touch data
        UpdateTouch = 0,
        Jump = 1,
        Reset = 2,
    }

    // https://en.wikipedia.org/wiki/Singleton_pattern
    // Make this class singleton to guarantee there is only one instance which is
    // accessible by other classes
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log("NetworkManager already exists");
                Destroy(value);
            }
        }

    }
    public Client Client { get; private set; }

    private void Awake()
    {
        // https://stackoverflow.com/questions/33787803/share-gameobjects-between-scenes
        // https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
        // Keep network manager to be used in the next scene
        DontDestroyOnLoad(this.gameObject);
        Singleton = this;
    }
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
    }

    public void Connect(String addr)
    {
        Client.Connect(addr);
    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    // This function is called by fix intervals, so arriving messages can be handled here
    private void FixedUpdate()
    {
        Client.Update();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }
}
