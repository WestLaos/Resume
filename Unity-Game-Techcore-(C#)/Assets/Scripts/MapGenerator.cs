using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Description: This scripts creates a randomly generated cave out of objects given to it.
 *              The object it uses the generate the cave must be uniform in size of equal scale
 *              in length and height. The object this script is place on will be the bottom left corner of the map.
 *              
 * Variables: GameObject mapComponent: Stores the object this function duplicates to make the map out of.
 *            float mapComponentSize: Stores the size of the map component.
 *            int width: Stores the width of the map this is measured in the number of mapComponents you wish the map to be made out off
 *            int height: Stores the height of the mpa this is measured in the number of mapComponents you wish the map to be made out off
 *            float baseHeight: Stores a decimal representation of what percentage of the bottom of the map should be filled in
 *            int[,] grid: This varable holds a grids of 0,1 of the size width x height a zero represents an empty space and a 1 represents 
 *                         a map component in a given location.
 *            GameObject[,] objectGrid: This variable stores a 2d grid containing all object that make up the generated map
 *            bool useSeed: indicates whether the map generation will use a specified seed or a random one
 *            int seed: used as seed for map generation
 *            System.Random rand: used to generate all random values in the program
 */


public class MapGenerator : MonoBehaviour
{
    // Stores the object that the map is made of
    public GameObject mapComponent;

    // stores size/scale of map component
    public float mapComponentSize = 1;

    // Stores the range of this map
    public int width = 0;
    public int height = 0;

    // stores a decimal of what percent of the bottom of the map will be filled in
    public float baseHeight = .125f;

    // An array representing the map
    private int[,] grid;
    // Stores objects making up the map
    private GameObject[,] objectGrid;

    // Handles Random Values
    public bool useSeed = false;
    public int seed = 0;
    private System.Random rand;

    // colors of object in game // 84 105 227
    private Color32 GREEN = new Color32(153, 250, 129, 255); // 153, 230, 79, 255
    private Color32 DARK_GREEN = new Color32(104, 205, 110, 255); // 104, 185, 60, 255
    private Color32 BROWN = new Color32(138, 104, 104, 255); // 138, 84, 54, 255
    private Color32 LIGHT_BROWN = new Color32(151, 104, 100, 255); // 151, 84, 50, 255
    private Color32 DARK_BROWN = new Color32(117, 84, 109, 255); // 117, 64, 59, 255
    private Color32 DARKEST_BROWN = new Color32(100, 79, 98, 255); // 100, 59, 48, 255

    // Start is called before the first frame update
    void Start()
    {
        // Checks if map seed if predetermined if so loads that seed
        // if not then loads random seed.
        if (useSeed) {
            rand = new System.Random(seed);
        } else {
            rand = new System.Random();
        }

        // Instantiates global variables grid and objectGrid
        this.grid =  new int[width, height];
        this.objectGrid = new GameObject[width, height];

        // Runs code to fill grid
        GenerateMap();
    }

    // Randomly adds values to the grid based on the randomFillPercent value
    void randomFillGrid()
    {
   
        // stores the cut off for where all the grid will be filled
        int topOfBase = (int)(baseHeight * height);

        // randomly fills values above the base
        for (int x = 0; x < width; x++) { // itterates through all x values in grid
            for (int y = 0; y < height; y++) { // itterates through all y values >= topOfBase
                // Decides if grid will be filled. Odds of filling a spot decrease as y increases
                // All spots bellow topOfBase are filled
                if (y < topOfBase) {
                    grid[x, y] = 1;
                } else if (rand.Next(1, 101) < height - (y * .9)) {
                    grid[x, y] = 1;
                } else {
                    grid[x, y] = 0;
                }
            }
        }
    }

    // Generates the map for the game
    void GenerateMap() 
    {
        // randomly fills grid with values
        randomFillGrid();

        // cleans up map in different ways
        int mapStyle = rand.Next(1, 6);
        for (int i = 0; i < 3; i++) 
        {
            if (mapStyle == 1)
            {
                CleanMapMiddleOut();
                //Debug.Log("Middle Out");
            }
            else if (mapStyle == 2)
            {
                CleanMapLeftToRight();
                //Debug.Log("Left To Right");
            }
            else if (mapStyle == 3)
            {
                CleanMapRightToLeft();
                //Debug.Log("Right To Left");
            }
            else if (mapStyle == 4)
            {
                CleanMapSidesIn();
                //Debug.Log("Sides In");
            }
            else 
            {
                CleanMapBottomUp();
                //Debug.Los("Bottom up");
            }
        }
        for (int i = 0; i < 2; i++)
        {
            CleanMap();
        }

        // goes through map and creates objects and selects the correct material
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == 1)
                {
                    Vector3 position = new Vector2(this.transform.position.x + x * mapComponentSize,
                        this.transform.position.y + y * mapComponentSize);
                    GameObject newObject = Instantiate(mapComponent, position, mapComponent.transform.rotation);
                    objectGrid[x, y] = newObject;
                    // if this is one of the top 2 blocks sets color to green
                    int objectsAbove = CountObjectAbove(x, y);
                    if (objectsAbove < 2)
                    {
                        newObject.GetComponent<Renderer>().material.SetColor("_Color", GREEN);
                    }
                    // if this is 2 blocks down sets it to transition green
                    else if (objectsAbove == 2)
                    {
                        newObject.GetComponent<Renderer>().material.SetColor("_Color", DARK_GREEN);
                    }
                    // if between 2 and 14 blocks sets it to brown or acccent brown
                    else if (objectsAbove < 13)
                    {
                        if (Random.Range(1, 21) == 1)
                        {
                            newObject.GetComponent<Renderer>().material.SetColor("_Color", BROWN);
                        }
                        else
                        {
                            newObject.GetComponent<Renderer>().material.SetColor("_Color", LIGHT_BROWN);
                        }
                    }
                    // if 14 blocks down sets to transition brown or if more than 14 blocks down and left blocks isnt tranistion brown
                    else if (objectsAbove == 13 || 
                        (x > 0 && grid[x - 1, y] == 1
                        && objectsAbove > 13 && objectGrid[x - 1, y].GetComponent<Renderer>().material.color.Equals(LIGHT_BROWN)))
                    {
                        newObject.GetComponent<Renderer>().material.SetColor("_Color", DARK_BROWN);
                    }
                    else
                    {
                        newObject.GetComponent<Renderer>().material.SetColor("_Color", DARKEST_BROWN);
                    }
                    if (y < 2)
                    {
                        newObject.GetComponent<Renderer>().material.SetColor("_Color", DARKEST_BROWN);
                    }
                    if (y > 0 && grid[x, y - 1] == 1 && objectGrid[x, y - 1].GetComponent<Renderer>().material.color.Equals(DARKEST_BROWN) 
                        && !objectGrid[x, y].GetComponent<Renderer>().material.color.Equals(DARKEST_BROWN)) 
                    {
                        objectGrid[x, y].GetComponent<Renderer>().material.SetColor("_Color", DARK_BROWN);
                    }
                }
            }
        }
    }

    void CleanMapBottomUp() 
    {
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width / 2; x++)
            {
                int surroundingComponentesLeft = CountSurroundingComponents((width / 2) - x, y);
                int surroundingComponentesRight = CountSurroundingComponents((width / 2) + x, y);

                // checks the left side 
                if (surroundingComponentesLeft >= 4)
                {
                    grid[(width / 2) - x, y] = 1;
                }
                else if (surroundingComponentesLeft < 4)
                {
                    grid[(width / 2) - x, y] = 0;
                }

                // checks the right side
                if (surroundingComponentesRight >= 4)
                {
                    grid[(width / 2) + x, y] = 1;
                }
                else if (surroundingComponentesRight < 4)
                {
                    grid[(width / 2) + x, y] = 0;
                }
            }
        }
    }

    void CleanMapMiddleOut() 
    {
        for (int x = 0; x < width / 2; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingComponentesLeft = CountSurroundingComponents((width / 2) - x, y);
                int surroundingComponentesRight = CountSurroundingComponents((width / 2) + x, y);
                
                // checks the left side 
                if (surroundingComponentesLeft >= 4)
                {
                    grid[(width / 2) - x, y] = 1;
                } 
                else if (surroundingComponentesLeft < 4)
                {
                    grid[(width / 2) - x, y] = 0;
                }

                // checks the right side
                if (surroundingComponentesRight >= 4)
                {
                    grid[(width / 2) + x, y] = 1;
                }
                else if (surroundingComponentesRight < 4)
                {
                   grid[(width / 2) + x, y] = 0;
                }
            }
        }
    }

    void CleanMapSidesIn()
    {
        for (int x = 0; x < width / 2; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingComponentesLeft = CountSurroundingComponents(x, y);
                int surroundingComponentesRight = CountSurroundingComponents((width - 1 - x), y);

                // checks the left side 
                if (surroundingComponentesLeft >= 4)
                {
                    grid[x, y] = 1;
                }
                else if (surroundingComponentesLeft < 4)
                {
                    grid[x, y] = 0;
                }

                // checks the right side
                if (surroundingComponentesRight >= 4)
                {
                    grid[(width - 1 - x), y] = 1;
                }
                else if (surroundingComponentesRight < 4)
                {
                    grid[(width - 1 - x), y] = 0;
                }
            }
        }
    }

    void CleanMapLeftToRight()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingComponentes = CountSurroundingComponents(x, y);

                // checks the left side 
                if (surroundingComponentes >= 4)
                {
                    grid[x, y] = 1;
                }
                else if (surroundingComponentes < 4)
                {
                    grid[x, y] = 0;
                }
            }
        }
    }

    void CleanMapRightToLeft()
    {
        for (int x = (width - 1); x >= 0; x--)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingComponentes = CountSurroundingComponents(x, y);

                // checks the left side 
                if (surroundingComponentes >= 4)
                {
                    grid[x, y] = 1;
                }
                else if (surroundingComponentes < 4)
                {
                    grid[x, y] = 0;
                }
            }
        }
    }

    int CountSurroundingComponents(int x, int y) 
    {
        int count = 0;
        for (int surroundingX = x - 1; surroundingX <= x + 1; surroundingX++) 
        {
            for (int surroundingY = y - 1; surroundingY <= y + 1; surroundingY++)
            {
                if (surroundingX >= 0 && surroundingX < width && surroundingY >= 0 && surroundingY < height)
                {
                    if (grid[surroundingX, surroundingY] == 1)
                    {
                        count += 1;
                    }
                }
            }
        }

        return count;
    }



    void CleanMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingComponentes = CountSurroundingComponents(x, y);

                // checks the left side 
                if (surroundingComponentes > 4)
                {
                    grid[x, y] = 1;
                }
                else if (surroundingComponentes < 4)
                {
                    grid[x, y] = 0;
                }
            }
        }
    }

    int CountObjectAbove(int x, int y)
    {
        int count = 0;
        while ((count + y) < (height - 1))
        {
            if (grid[x, y + count + 1] == 1)
            {
                count += 1;
            }
            else
            {
                return count;
            }
        }
        return count;
    }
}
