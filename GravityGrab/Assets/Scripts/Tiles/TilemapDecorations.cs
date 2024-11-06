using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDecorations : MonoBehaviour
{
    public Transform player;              // Assign the player's Transform in the Inspector
    public float swayAmount = 0.05f;      // Amount of sway movement
    public float swaySpeed = 3f;          // Speed of the sway
    public float detectionRadius = 3f;    // How close the player needs to be to trigger sway

    private Tilemap tilemap;
    private Vector3[,] initialPositions;  // Store initial positions of each tile

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Store the initial world position of each tile in the Tilemap
        BoundsInt bounds = tilemap.cellBounds;
        initialPositions = new Vector3[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))  // Only store positions for non-empty tiles
                {
                    Vector3 worldPos = tilemap.CellToWorld(cellPos);
                    initialPositions[x - bounds.xMin, y - bounds.yMin] = worldPos;
                }
            }
        }
    }

    void Update()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))  // Only process non-empty tiles
                {
                    Vector3 worldPos = tilemap.CellToWorld(cellPos);
                    Vector3 initialPos = initialPositions[x - bounds.xMin, y - bounds.yMin];

                    float distanceToPlayer = Vector3.Distance(worldPos, player.position);

                    if (distanceToPlayer < detectionRadius)
                    {
                        // Apply a small, oscillating movement (sway)
                        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
                        tilemap.SetTransformMatrix(cellPos, Matrix4x4.TRS(new Vector3(sway, 0, 0), Quaternion.identity, Vector3.one));
                    }
                    else
                    {
                        // Reset to initial position when player is far away
                        tilemap.SetTransformMatrix(cellPos, Matrix4x4.identity);
                    }
                }
            }
        }
    }
}
