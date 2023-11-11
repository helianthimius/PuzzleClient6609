using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Riptide;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Connect the server using the given IP Address and port
    public void Connect()
    {
        // https://gamedev.stackexchange.com/questions/132569/how-do-i-find-an-object-by-type-and-name-in-unity-using-c
        // Find the input object and read the text value
        var addr = GameObject.Find("AddressInput").GetComponent<TMP_InputField>().text;
        NetworkManager.Singleton.Client.Connect(addr);
    }
    public void Jump()
    {
        Message message = Message.Create(MessageSendMode.Unreliable, (ushort)NetworkManager.MessageType.Jump);
        NetworkManager.Singleton.Client.Send(message);
    }
}
