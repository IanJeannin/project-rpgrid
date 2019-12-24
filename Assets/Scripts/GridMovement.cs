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

    private bool pauseMovement = false; //Prevents movement while true
    private float xMovement = 0; //Marks if player is moving horizontally
    private float yMovement = 0; //Marks if player is moving vertically

    private void FixedUpdate()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

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
        if (groundTilemap.GetTile(groundTilemap.WorldToCell(newPosition))!=null)
        {
            transform.position = newPosition;
        }
        StartCoroutine(PauseMovement());
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
