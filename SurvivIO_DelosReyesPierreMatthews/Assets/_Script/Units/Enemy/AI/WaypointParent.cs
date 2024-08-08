using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointParent : GenericSingletonClass<WaypointParent>
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i)); // add location to path
            waypoints[i].GetComponent<SpriteRenderer>().enabled = false; // hides nodes when playing
        }
    }

    // Automated adjusting of node names
    private void Reset()
    {
        Debug.LogWarning("Reset");
        int index = 0;
        while (index < transform.childCount)
        {
            transform.GetChild(index).name = "Node " + index.ToString();
            index++;
        }
    }

    public List<Transform> GetWayPoints()
    {
        return waypoints;
    }

}
