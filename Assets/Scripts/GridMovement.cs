using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [Tooltip("The tilemap that movement takes place on.")]
    [SerializeField]
    private Tilemap groundTilemap;
    [Tooltip("How long to pause movement for after a move.")]
    [SerializeField]
    private float pauseMovementTime;
    [Tooltip("The character script attached to the game object.")]
    [SerializeField]
    private Character movingCharacter;
    [Tooltip("How much AP each square of movement costs.")]
    [SerializeField]
    private int movementCost=1;

    private bool pauseMovement = false; //Prevents movement while true
    private bool startOfTurn = true; //Will mark whether or not a turn has started and AP should be determined
    private float xMovement = 0; //Marks if player is moving horizontally
    private float yMovement = 0; //Marks if player is moving vertically
    private int tempAP; //Will store the AP used so far in the turn. 
    private List<Vector2> tilesMovedOn = new List<Vector2>(); //Stores the positions of the tiles already moved on

    private void FixedUpdate()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        //At start of turn, AP of the character is stored in a temporary variable
        if(startOfTurn==true)
        {
            startOfTurn = false;
            Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
            tilesMovedOn.Add(startingPosition); //Adds starting position to list of tiles moved on this turn. (This is so if the player moves back to their starting position, their ap used during movement will be restored)
            tempAP = movingCharacter.GetAP();
        }

        if (pauseMovement != true)
        {
            if (xMovement != 0)
            {
                yMovement = 0; //Makes sure that only horizontal movement is calculated
                Move(xMovement, yMovement);
            }
            else if (yMovement != 0)
            {
                xMovement = 0; //Ensures only vertical movement is calculated
                Move(xMovement, yMovement);
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (groundTilemap.GetTile(groundTilemap.WorldToCell(mousePosition)) != null)
            {
                Vector3Int tilePosition = groundTilemap.WorldToCell(mousePosition);
                ClickMove(tilePosition);
            }
        }
    }

    private void Move(float horizontal, float vertical)
    { 
        //Marks the position of where the object is moving to. 
        Vector2 newPosition = new Vector2(transform.position.x + horizontal, transform.position.y + vertical);
        if (groundTilemap.GetTile(groundTilemap.WorldToCell(newPosition))!=null) //If the space being moved to is not empty
        {
            if (tilesMovedOn.Contains(newPosition)) //If the space being moved to has already been passed through on this turn
            {
                int index = tilesMovedOn.IndexOf(newPosition); //Finds index of space in list
                //Removes all spaces from list up to the space being moved back onto, and restores AP up to however much was used to get to this space originally.
                for(int x=tilesMovedOn.Count-1; x>index; x--)
                {
                    tilesMovedOn.RemoveAt(x); 
                    tempAP += movementCost;
                    movingCharacter.UpdateAP(tempAP);
                }
                transform.position = newPosition; //Move the character to the tile
            }
            else if(tempAP>0) //Makes sure there is still ap remaining before moving to a new space
            {
                tilesMovedOn.Add(newPosition); //add the position of the tile moved on to the list
                transform.position = newPosition; //Move the character to the tile
                tempAP -= movementCost; //Subtract the cost of movement from this turns AP use.
                movingCharacter.UpdateAP(tempAP);
            }
            Debug.Log(tempAP);
        }
        StartCoroutine(PauseMovement());
    }

    //Iterates through list of 
    private void CheckList()
    {

    }

    private void ClickMove(Vector3Int destination)
    {
        transform.position = groundTilemap.GetCellCenterWorld(destination);
    }

    //Adds slight pause before movement continues
    private IEnumerator PauseMovement()
    {
        pauseMovement = true;
        yield return new WaitForSeconds(pauseMovementTime);
        pauseMovement = false;

    }
}
