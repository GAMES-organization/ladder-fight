using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerController player;
    public StackController ballStack;
    public GameObject playerChild;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    public void OnCollisionEnter(Collision collision)
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
    
    public void OnTriggerExit(Collider other)
    {
        
    }
    
}
