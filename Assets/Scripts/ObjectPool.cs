using System.Collections.Generic;
using UnityEngine;

/*
    Script: ObjectPool
    Author: Gareth lockett
    Version: 1.0
    Description:    Simple object pooling script (Eg it is not performant to keep Instantiate/Destroy objects! Hide instead of destroying and then reuse)
*/

public class ObjectPool : MonoBehaviour
{
    // Properties
    public GameObject targetGameObject;             // This is a reference to the object we want to have lots of instances of.
    public KeyCode spawnKey = KeyCode.Space;        // When this key is held down, spawn more objects.
    public float maximumDistance = 5f;              // Objects get deacivated when they are this far from the origin.
    public float moveSpeed = 3f;                    // Speed (in units per second) the objects will move outward.

    private List<GameObject> activeObjects;         // List of references to all the currently active objects.
    private List<GameObject> deactivatedObjects;    // List of references to all the currently deactivated objects.

    // Methods
    private void Update()
    {
        // Sanity checks.
        if( this.targetGameObject == null ) { return; }
        if( this.activeObjects == null ) { this.activeObjects = new List<GameObject>(); }
        if( this.deactivatedObjects == null ) { this.deactivatedObjects = new List<GameObject>(); }

        // Check if key is pressed.
        if( Input.GetKey( this.spawnKey ) == true )
        {
            // Check for a deactivated object to use.
            GameObject gameObject = null;
            if( this.deactivatedObjects.Count > 0 )
            {
                gameObject = this.deactivatedObjects[ 0 ];  // Get the first deactivated object.
                this.deactivatedObjects.RemoveAt( 0 );      // Remove the first object from the deactivated list.
            }
            else // Otherwise, instantiate a new object.
            {
                gameObject = GameObject.Instantiate( this.targetGameObject );
            }

            // Add the object to the active list and make active.
            this.activeObjects.Add( gameObject );       // Add the object to the active list.
            gameObject.SetActive( true );               // Set the object to be active (Eg visible)

            // Randomly position the game object somewhere around the origin. This will control the direction it moves in (Eg away from the origin)
            gameObject.transform.position = new Vector3( Random.Range( -0.1f, 0.1f ), Random.Range( -0.1f, 0.1f ), Random.Range( -0.1f, 0.1f ) );
        }

        // Loop through all the active objects moving them away from the origin. Once they reach a distance then deactivate them.
        for( int i = 0; i < this.activeObjects.Count; i++ )
        {
            // Move the object.
            this.activeObjects[ i ].transform.position += this.activeObjects[ i ].transform.position.normalized * Time.deltaTime * this.moveSpeed;

            // Check distance.
            if( Vector3.Distance( this.activeObjects[ i ].transform.position, Vector3.zero ) >= this.maximumDistance )
            {
                // Object is beyond the maximum distance ...
                this.activeObjects[ i ].SetActive( false );             // ... deactivate it
                this.deactivatedObjects.Add( this.activeObjects[ i ] ); // ... add it to the deactivated list
                this.activeObjects.RemoveAt( i );                       // ... remove it from the active list
                i--;    // ... deincrement i (because we just removed an element of the active list and it is 1 element shorter now)
            }
        }

    }
}
