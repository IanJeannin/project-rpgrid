using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;

    private IDictionary<Vector3, Vector3> tileParents = new Dictionary<Vector3, Vector3>();

    //After finding the shortest path, check how much movement it took in GridMovement, and if it takes too much ap, don't move.
    List<Vector3> FindShortestPath(Vector3 startPosition, Vector3 endPosition)
    {
        List<Vector3> path = new List<Vector3>();
        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> exploredTiles = new HashSet<Vector3>();
        queue.Enqueue(startPosition);

        while(queue.Count!=0)
        {
            Vector3 currentTile = queue.Dequeue();

            if(currentTile==endPosition)
            {
                Vector3 current = endPosition;
                while(!tileParents.ContainsKey(startPosition)) //Starting from the end, marks every tile in the path until the start is marked
                {
                    path.Add(current);
                    current = tileParents[current];
                }
                return path;
            }

            List<Vector3> tiles = GetPossibleTiles(currentTile);
            foreach(Vector3 tile in tiles)
            {
                if(!exploredTiles.Contains(tile)) //If the tile hasn't already been explored...
                {
                    exploredTiles.Add(tile); //Add to list of explored tiles.
                    tileParents.Add(tile, currentTile); //Relates the new tile with it's previous tile in tileParents
                    queue.Enqueue(tile); //Add to queue to search surrounding tiles.
                }
            }
        }
        return null; //If no path is found, returns null
    }

    List<Vector3> GetPossibleTiles(Vector3 currentTile)
    {
        //Adds every adjacent tile to a list
        List<Vector3> walkableTiles = new List<Vector3>()
        {
            new Vector3 (currentTile.x+1,currentTile.y),
            new Vector3 (currentTile.x-1,currentTile.y),
            new Vector3 (currentTile.x,currentTile.y+1),
            new Vector3 (currentTile.x,currentTile.y-1)
        };

        foreach(Vector3 tile in walkableTiles) //Checks every adjacent tile to ensure it is walkable, if not, removes it from the list
        {
            if(groundTilemap.GetTile(groundTilemap.WorldToCell(tile)) == null)
            {
                walkableTiles.Remove(tile);
            }
        }
        return walkableTiles;
    }
}
