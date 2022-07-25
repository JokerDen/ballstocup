using System.Collections.Generic;
using UnityEngine;

public class ReparenterTrigger : MonoBehaviour
{
    [SerializeField] Transform reparent;
    
    private List<Rigidbody> activated = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        var body = other.attachedRigidbody;
        if (body == null || activated.Contains(body)) return;
        
        activated.Add(body);
        
        body.transform.SetParent(reparent);
    }
}
