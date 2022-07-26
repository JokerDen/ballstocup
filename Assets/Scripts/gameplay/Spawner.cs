using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;

    private List<GameObject> items = new List<GameObject>();

    public void Spawn(int num)
    {
        for (int i = 0; i < num; i++)
            Spawn();
    }

    public void Spawn()
    {
        var item = Instantiate(prefab, position.position, position.rotation, transform);
        items.Add(item);
        item.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (position != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position.position, .5f);
        }
    }
}
