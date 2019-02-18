using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePos{
        TL,
        TR,
        BL,
        BR,
        T1,
        T2,
        L1,
        L2,
        R1,
        R2,
        B1,
        B2,
        CTL,
        CTR,
        CBL,
        CBR,
    }

[RequireComponent(typeof(SpriteRenderer))]

public class Tile
{
   
    TilePos tilePos;
    TileType tileType; 
    World world;

    int x;
    int y;

    

    public TileType Type{
        get{
            return tileType;
        }
        set{
            tileType = value;
        }
    }

    public TilePos Pos{
        get{
            return tilePos;
        }
        set{
            tilePos = value;
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


    public Tile(World w, int x , int y){
        this.world = w;
        this.x = x;
        this.y = y;
    }
}
