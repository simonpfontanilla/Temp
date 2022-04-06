using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapLevelClass
{
    public int level;

    // Gate
    public List<string> gatePositons;
    public List<int> whichGates;
    public List<string> gateTexts;
    public int maxShips; // Used for enemy count
    public int numGatesSpawners;
    // public string bigStartGate;

    // public string centerCarrier;

    // Asteroid
    public List<string> asteroidPositons;

    // Collectable
    public List<string> collectablePositons;
}
