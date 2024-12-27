using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSingleton : Singleton<ColourSingleton>
{
    // Same problem as UIManager singleton (another quick fix)
    // But gets destroyed by DifficultyLevel if both have singleton script attached (now deleted script)
    // TODO fix if time
}
