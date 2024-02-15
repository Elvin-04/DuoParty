using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Cards : ScriptableObject
{
    public string cardName;
    public string type;
    public Sprite cardImage;
    public cardTypes cardType;
    public cardcolors cardColor;

    public Path greenPath;
    public Path redPath;

    [HideInInspector] public Path instantiatedGreenPath;
    [HideInInspector] public Path instantiatedRedPath;

    private List<Path> pathList = new();

    private void Awake()
    {
        ResetPaths();
    }


    private void ResetPaths()
    {
        pathList.Clear();

        instantiatedGreenPath = greenPath;
        instantiatedRedPath = redPath;

        pathList.Add(instantiatedGreenPath);
        pathList.Add(instantiatedRedPath);
    }

    private void ApplyPath()
    {
        instantiatedGreenPath = pathList[0];
        instantiatedRedPath = pathList[1];
    }

    public void TurnRight()
    {
        for (int i = 0; i< pathList.Count; i++)
        {
            var p = pathList[i];

            bool temp = p.canMoveUp;
            p.canMoveUp = p.canMoveLeft;
            p.canMoveLeft = p.canMoveDown;
            p.canMoveDown = p.canMoveRight;
            p.canMoveRight = temp;

            pathList[i] = p;
        }

        ApplyPath();
    }

    public void TurnLeft()
    {
        for (int i = 0; i < pathList.Count; i++)
        {
            var p = pathList[i];

            bool temp = p.canMoveUp;
            p.canMoveUp = p.canMoveRight;
            p.canMoveRight = p.canMoveDown;
            p.canMoveDown = p.canMoveLeft;
            p.canMoveLeft = temp;

            pathList[i] = p;
        }

        ApplyPath();
    }

    public void ResetRotation()
    {
        ResetPaths();
    }


}

public enum cardTypes
{
    corridor = 0,
    corner = 1,
    tPath = 2,
    cross = 3,

}

public enum cardcolors
{
    red = 0,
    green = 1,
    redAndGreen = 2,
    none = 3,

}
