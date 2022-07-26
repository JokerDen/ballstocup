using System.Collections.Generic;
using UnityEngine;

public class ResizerTrigger : MonoBehaviour
{
    [SerializeField] float scaleModifier;
    
    private List<Rigidbody> activated = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        var body = other.attachedRigidbody;
        if (body == null || body.isKinematic || activated.Contains(body)) return;
        
        activated.Add(body);
        
        body.transform.localScale *= scaleModifier;
    }
}
