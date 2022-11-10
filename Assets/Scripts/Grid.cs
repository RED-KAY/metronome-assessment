using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Cell cellPrefab;
    public IntVector2 size;

    Cell[,] cells;
    List<Cell> activeCells = new List<Cell>();

    [Range(0f, 1f)]
    public float edgeProbability;

    [Range(0f, 1f)]
    public float generationStepDelay = 0.5f;

    private void Start()
    {
        Generate();
        StartCoroutine(InitChildObjects());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ReStart();
        }
    }

    private void ReStart()
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Generate();
        StartCoroutine(InitChildObjects());
    }

    void Generate()
    {
        cells = null;
        cells = new Cell[size.x, size.y];
        activeCells.Clear();
        for(int i=0; i<size.y; i++)
        {
            for(int j=0; j<size.x; j++)
            {
                Cell cell = GameObject.Instantiate(cellPrefab, transform);
                cell.transform.localPosition = new Vector2(j, i);
                cell.coordinates.x = j;
                cell.coordinates.y = i;
                cell.name = j + ", " + i;
                activeCells.Add(cell);
                cells[j, i] = cell;
            }
        }
    }

    IEnumerator InitChildObjects()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        if (activeCells.Count == 0)
            yield return delay;
        foreach (Cell cell in activeCells)
        {
            yield return delay;
            //Debug.Log(cell.coordinates.x + ", " + cell.coordinates.y);
            
            if (cell.Other != null)
                continue;
            else
                cell.spriteRenderer.color = cell.defaultColor;
            List<GridDirection> possibleDirections = GetPossibleDirections(cell);
            
            GridDirection gridDirection = Random.value > edgeProbability ? GridDirection.None : possibleDirections[Random.Range(0, possibleDirections.Count)];
            if (gridDirection == GridDirection.None)
                continue;

            CreateEdge(cell, gridDirection);
        }
    }

    //This function basically scales the child object, changes color and hides child object of other cell. plus house keeping operations.
    void CreateEdge(Cell cell, GridDirection gridDirection)
    {
        Transform child = cell.child;
        child.localRotation = GridDirections.ToRotation(gridDirection);
        child.localScale = child.localScale * 2;
        child.localPosition = (child.localPosition + GridDirections.ToIntVector2(gridDirection)) / 2;
        IntVector2 otherCellCoord = cell.coordinates + GridDirections.ToIntVector2(gridDirection);
        Cell otherCell = cells[otherCellCoord.x, otherCellCoord.y];
        cell.Other = otherCell;
        otherCell.Other = cell;
        otherCell.child.gameObject.SetActive(false);
    }

    List<GridDirection> GetPossibleDirections(Cell cell)
    {
        List<GridDirection> possibleDirections = new List<GridDirection>();

        for (int i = 0; i < GridDirections.Count; i++)
        {
            IntVector2 coordinate = cell.coordinates + GridDirections.ToIntVector2((GridDirection)i);
            if (ContainsCoordinates(coordinate))
            {
                if (cells[coordinate.x, coordinate.y].Other != null)
                    continue;                
                possibleDirections.Add((GridDirection)i);
            }
        }

        if (possibleDirections.Count == 0)
            possibleDirections.Add(GridDirection.None);

        return possibleDirections;
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
    }
}
