using UnityEngine;
using UnityEngine.Android;
using Riptide;
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
                // https://docs.unity3d.com/ScriptReference/Screen.html
                // Joystick is enabled in lower right quarter
                if (touch.rawPosition.x > Screen.width / 2 && touch.rawPosition.y < Screen.height / 2)
                {

                    var moveArrow = touch.position - touch.rawPosition;
                    Message message = Message.Create(MessageSendMode.Unreliable, (ushort)NetworkManager.MessageType.UpdateTouch);

                    message.AddInt((int)moveArrow.x);
                    message.AddInt((int)moveArrow.y);

                    NetworkManager.Singleton.Client.Send(message);
                }
            }
            else if (touch.phase == UnityEngine.TouchPhase.Canceled || touch.phase == UnityEngine.TouchPhase.Ended)
            {
                Message message = Message.Create(MessageSendMode.Unreliable, (ushort)NetworkManager.MessageType.UpdateTouch);

                message.AddInt((int)0);
                message.AddInt((int)0);

                NetworkManager.Singleton.Client.Send(message);
            }
        }

    }
}
