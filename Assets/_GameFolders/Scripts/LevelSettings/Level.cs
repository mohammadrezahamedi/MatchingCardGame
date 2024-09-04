using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level Data", order = 1)]
public class Level : ScriptableObject
{
    public int Column; 
    public int Row;    
}