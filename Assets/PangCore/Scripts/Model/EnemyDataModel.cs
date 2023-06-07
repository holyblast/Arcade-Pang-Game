using UnityEngine;

/// <summary>
/// A class model that is used to retreive json data.
/// </summary>
[System.Serializable]
public class EnemyDataModel
{
    public Vector2 startingPosition;
    public Color32 color;
    public BubbleSizeType bubbleSize;
    public MoveDirectionType initialDirection;
}
