using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager Instance;

    private GameObject object1;
    private GameObject object2;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void UpdateCollisionobjects(GameObject newObject)
    {
        if (newObject == null) return;
        if (object1 == null)
        {
            object1 = newObject;
        } 
        else if (object2 == null)
        {
            object2 = newObject;
        }
        else
        {
            Debug.Log("Both objects are already set");
        }
    }

    private void FixedUpdate()
    {
        if(object1 != null && object2 != null)
        {
            ResolveCollision();
        }
    }

    private GameObject ResolveCollision()
    {
        Vector3 newPos = object1.transform.position - object2.transform.position;
        //Check what the next object should be

        //initiate new object with the new position


        object1 = null;
        object2 = null;
        return null;
    }
}