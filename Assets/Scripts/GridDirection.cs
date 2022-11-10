using UnityEngine;

public enum GridDirection
{
	North,
	East,
	South,
	West,
	None
}

public static class GridDirections
{

	public const int Count = 4;

	public static GridDirection RandomValue {
		get {
			return (GridDirection)Random.Range(0, Count);
		}
	}

	private static GridDirection[] opposites = {
		GridDirection.South,
		GridDirection.West,
		GridDirection.North,
		GridDirection.East
	};

	public static GridDirection GetOpposite(this GridDirection direction)
	{
		return opposites[(int)direction];
	}

	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};

	public static IntVector2 ToIntVector2(this GridDirection direction)
	{
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0, 270f),
		Quaternion.Euler(0f, 0, 180f),
		Quaternion.Euler(0f, 0, 90f)
	};

	public static Quaternion ToRotation(this GridDirection direction)
	{
		return rotations[(int)direction];
	}
}