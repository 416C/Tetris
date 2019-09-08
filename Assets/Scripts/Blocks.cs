using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    float lastfall = 0;
    void Start()
    {
        if (!isValidPos())
        {
            Debug.Log("GameOver");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a"))
        {
            Debug.Log("Left Key Pressed");
            //modify position
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d"))
        {
            Debug.Log("Right Key Pressed");
            //modify position
            transform.position += new Vector3(1, 0, 0);
            if (isValidPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w"))
        {
            Debug.Log("Up Key Pressed");
            //modify position
            transform.Rotate(0, 0, -90);

            if (isValidPos())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s") || Time.time - lastfall >= 1)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidPos())
                {
                    updateGrid();
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                    Playfield.deleteAllRows();
                    FindObjectOfType<Spawner>().spawnerNext();
                    enabled = false;
                }
                lastfall = Time.time;
            }

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
                    if (Playfield.grid[x, y] != null)
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