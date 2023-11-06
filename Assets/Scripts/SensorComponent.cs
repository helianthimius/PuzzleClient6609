using UnityEngine;
using UnityEngine.Android;
using RiptideNetworking;
using System;


public class SensorComponent : MonoBehaviour
{
    void Update()
    {
        // TOUCH
        // Checks if user is touching the screen
        if (Input.touchCount > 0)
        {
            // https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Touch.html
            // Multi-touch is handled. Just handling the first finger.
            var touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                var moveArrow = touch.position - touch.rawPosition;
                Message message = Message.Create(MessageSendMode.unreliable, (ushort)NetworkManager.MessageType.UpdateTouch);

                message.AddInt((int)moveArrow.x);
                message.AddInt((int)moveArrow.y);

                NetworkManager.Singleton.Client.Send(message);
            }
        }

    }
}
