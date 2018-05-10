using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for all objects able to be killed (enemies and player)
/// </summary>
public interface IKillable
{
    /// <summary>
    /// Appropriate handle death of character
    /// </summary>
    void Die();	
}
