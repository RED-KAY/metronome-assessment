using UnityEngine;

public class Cell : MonoBehaviour
{
    public IntVector2 coordinates;

    // This is to signify an edge - in case if this cell's child object is chosen to scale then it'll coincide with the cell adjacent to it.
    //In that case you want to connect this cell and that cell, forming an edge that connect 2 cells, and remove child object of that cell. This field is a reference to that cell.
    Cell other = null;

    public Cell Other {
        get {
            return other;
        }
        set {
            other = value;
            spriteRenderer.color = secondaryColor;
        }
    }

    public Color defaultColor;
    public Color secondaryColor;
    public SpriteRenderer spriteRenderer;

    public Transform child;
}
