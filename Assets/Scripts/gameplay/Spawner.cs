using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform container;
    public float radius;

    private List<GameObject> items = new List<GameObject>();

    public void Spawn(int num)
    {
        // TODO: Poisson Disc Sampling to generate balls inside circle/sphere

        var body = prefab.GetComponent<Rigidbody>();
        bool fixedZ = (body.constraints & RigidbodyConstraints.FreezePositionZ) != RigidbodyConstraints.None;
        
        for (int i = 0; i < num; i++)
        {
            // var pos = container.position + Random.insideUnitSphere * radius;
            Vector3 offset = Vector3.zero;
            if (fixedZ)
                offset = Random.insideUnitCircle * radius;
            else
                offset = Random.insideUnitSphere * radius;
            
            var pos = container.position + offset;
            var item = Instantiate(prefab, pos, container.rotation, container);
            items.Add(item);
            item.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        if (container != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(container.position, radius);
        }
    }
}
