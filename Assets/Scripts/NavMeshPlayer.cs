using UnityEngine;
using UnityEngine.AI;

/*
    Script: NavMeshPlayer
    Author: Gareth Lockett
    Version: 1.0
    Description:    Simple example scrip for using NavAgents with NavMesh.
                    Raycast from the camera on mouse click and if hit object has its' layer set, have this game objects NavMesh agent to move the hit point.
                    Make sure to set
*/

[ RequireComponent( typeof( NavMeshAgent ) ) ]
public class NavMeshPlayer : MonoBehaviour
{
    // Properties
    public Camera cam;                          // Reference to the camera.
    public LayerMask floorLayers;               // Objects on these layers will allow the NavMeshAgent to walk on them.

    private NavMeshAgent navMeshAgent;          // Reference to the NavMeshAgent component on this game object.

    // Methods
    private void Start()
    {
        // Cache a reference to the NavMeshAgent component.
        this.navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Sanity checks.
        if( this.cam == null ) { return; }
        if( this.navMeshAgent == null ) { return; } //?

        // Check for a mouse click.
        if( Input.GetMouseButton( 0 ) == true )
        {
            // Create ray from camera into the scene.
            Ray ray = this.cam.ScreenPointToRay( Input.mousePosition );

            // Create raycast variable for storing hit information.
            RaycastHit hit;

            // Do raycast.
            if( Physics.Raycast( ray, out hit, Mathf.Infinity, this.floorLayers ) == true )
            {
                // Set the target move destination for the NavMeshAgent to the raycast hit point.
                this.navMeshAgent.SetDestination( hit.point );
            }
        }
    }
}
