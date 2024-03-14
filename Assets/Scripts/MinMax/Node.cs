using System.Collections;
using System.Collections.Generic;
using Managers;
using MinMax;
using MonoBehaviours;
using UnityEngine;

public class Node
{
    private Side _owner;
    public Side _turn;
    private Cell[,] _matrix;

    public Node(Side owner, Side turn, Cell[,] matrix)
    {
        _owner = owner;
        _turn = turn;
        _matrix = matrix;
    }

    public int GetHeuristicValue()
    {
        int heuristicValue = 0;
        foreach (Cell cell in _matrix)
        {
            if (cell == null) continue;
            if (cell.Occupant == null) continue;

            //  heuristicValue = cell.Occupant.Side == _owner ? cell.Occupant.HeuristicScore : -cell.Occupant.HeuristicScore;
            if (cell.Occupant.Side == _owner)
                heuristicValue += cell.Occupant.HeuristicScore;
            else
                heuristicValue -= cell.Occupant.HeuristicScore;
        }

        return heuristicValue;
    }

    public bool IsTerminal()
    {
        return GetChilds().Count == 0;
    }

    public List<Node> GetChilds()
    {
        List<Node> nodeList = new List<Node>();

        foreach (Cell cell in _matrix)
        {
            if (cell == null) continue;
            if (cell.Occupant == null) continue;
            if (cell.Occupant.Side == _owner) continue;
        }

        return nodeList;
    }
}
