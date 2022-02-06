using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace Assets.Scenes.code.NPCCode
{
    public class NPCPatrolPoint : MonoBehaviour
    {
        [SerializeField]
        protected float debugRadius = 1f;

        [SerializeField]
        protected float connectivityRadius = 50f;

        List<NPCPatrolPoint> connections;

        public void Start()
        {
            //grabs all waypoints in the scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //create a list of waypoints i can refer to later
            connections = new List<NPCPatrolPoint>();

            //check if they are a connected waypoint
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                NPCPatrolPoint nextWaypoint = allWaypoints[i].GetComponent<NPCPatrolPoint>();

                //if we find a waypoint
                if (nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint != this)
                    {
                        connections.Add(nextWaypoint);
                    }
                }
            }
        }

        
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, connectivityRadius);
        }

        public NPCPatrolPoint NextWaypoint(NPCPatrolPoint previousWaypoint)
        {
            if (connections.Count == 0)
            {
                //no waypoints left
                Debug.LogError("No Waypoints left");
                return null;
            }
            else if (connections.Count == 1 && connections.Contains(previousWaypoint))
            {
                //if there is only one waypoint and it's the previous one, use it
                return previousWaypoint;
            }
            else
            {
                //otherwise find a random one that isn't the previous one
                NPCPatrolPoint nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, connections.Count);
                    nextWaypoint = connections[nextIndex];

                } while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }
    }
}
