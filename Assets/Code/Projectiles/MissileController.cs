using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls missile movement and destruction functionality
/// </summary>
public class MissileController : MonoBehaviour
{
    


    

    [SerializeField]
    [Tooltip("The speed that missiles travel")]
    private float missileSpeed = 0.2f;

    [SerializeField]
    private float chanceOfDrop = 1f;
    

    void Start()
    {
        //missileTransform = GetComponent<Transform>();
    }
    
    void Update()
    {
        
    }

    

    
    
    
}
