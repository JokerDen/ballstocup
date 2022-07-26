using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace gameplay
{
    public class BallsCounter : MonoBehaviour
    {
        public CountEvent onNumChanged = new CountEvent();
        
        public int Num => counted.Count;
        
        public List<Rigidbody> counted = new List<Rigidbody>();

        private void OnTriggerStay(Collider other)
        {
            var body = other.attachedRigidbody;
            if (body == null || body.isKinematic || counted.Contains(body)) return;
            
            counted.Add(body);
            onNumChanged.Invoke(Num);
        }
    }

    public class CountEvent : UnityEvent<int>
    {
        
    }
}