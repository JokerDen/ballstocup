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
        for (int i = 0; i < num; i++)
            Spawn();
    }

    public void Spawn()
    {
        var item = Instantiate(prefab, container.position + Random.insideUnitSphere * radius, container.rotation, container);
        items.Add(item);
        item.SetActive(true);
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
