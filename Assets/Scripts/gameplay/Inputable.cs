using UnityEngine;

namespace gameplay
{
    public class Inputable : MonoBehaviour
    {
        public void HandleMove(float deltaX, float deltaY)
        {
            BroadcastMessage("OnInputDeltaX", deltaX, SendMessageOptions.DontRequireReceiver);
            BroadcastMessage("OnInputDeltaY", deltaY, SendMessageOptions.DontRequireReceiver);
        }
    }
}