using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit
{
        TileType tileType;
        World thisworld;
        int x;
        int y;
        Tile[] tiles;
        

        
    public Unit(World world, int x , int y){
        this.thisworld = world;
        this.x = x;
        this.y = y;
    }


    public int ID{get;set;}

       public TileType Type{
        get{
            return tileType;
        }
        set{
            tileType = value;
        }
    }


       public int X{
        get{
            return x;
        }
        set{
            x = value;
        }
    }
    public int Y{
        get{
            return y;
        }
        set{
            y = value;
        }
    }

    

}
