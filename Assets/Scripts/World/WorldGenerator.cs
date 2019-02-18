using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{   

    World world;
    int unitX = 60;
    int unitY = 60;
    int numBiomeType = 6;
    int numBiome = 18;
    
    // Start is called before the first frame update
    void Start()
    {   
        
        world = new World(); 
        world.LoadResources();
        world.GenerateWorld(unitX,unitY,numBiomeType,numBiome);
        
    }

  
}
