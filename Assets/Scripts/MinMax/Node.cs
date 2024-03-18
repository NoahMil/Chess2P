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
            if (cell.Occupant.Side == _turn) continue;
            cell.Occupant.AvailableMoves()
            // Je dois récupérer le mouvement de chaque Occupant dont c'est le tour et créer un nouveau node basé sur
            // celui du node actuelle et jouer le coup sur celui ci
            // 1 - Récupérer la liste des muvement possible pour cette matrice
            // 2 - Copier la matrice actuel
            // 3 - Jouer le mouvement sur la matrice actuel
            // 4 - Créer un Node contenant tout les information nécessaire (la nouvelle matrice, les sides)
            // 5 - Ajouter le node a la liste des enfants à retourner
            
            Side nextTurn = _turn == Side.Light ? Side.Dark : Side.Light;
            Node childNode = new Node(_owner, )
            nodeList.Add(childNode);
        }

        return nodeList;
    }
}
