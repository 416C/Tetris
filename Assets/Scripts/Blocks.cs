using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isValidPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            //not inside border
            if (!Playfield.inBorder(child.position))
            {
                return false;
            }

            //block in grid cell
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
            
        }

        return true;
    }

    void updateGrid()
    {
        //remove oldchildren from grid
        for (int y = 0; y < Playfield.height; y++)
        {
            for (int x = 0; x < Playfield.width; x++)
            {
                if(Playfield.grid[x,y] != null)
                {
                    if (Playfield.grid[x, y].parent == transform)
                    {
                        Playfield.grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

}
