using System.Collections.Generic;
using Data;
using Enums;
using Managers;
using UnityEngine;

public class Node
{ 
    public  Side OpponentTurn => (_owner == Side.Light ? Side.Dark : Side.Light);
    private Side _owner;
    private Piece[,] _matrix;
    private Coordinates _destination;
    private Coordinates _origin;
    public Coordinates Destination
    {
        get => _destination;
        set => _destination = value;
    }
    public Coordinates Origin
    {
        get => _origin;
        set => _origin = value;
    }


    public Node(Side owner, Piece[,] matrix, Coordinates origin, Coordinates destination)
    {
        _owner = owner;
        _matrix = matrix;
        _origin = origin;
        _destination = destination;
    }

    public float GetHeuristicValue()
    {
        float heuristicValue = 0f;
        foreach (Piece piece in _matrix)
        {
            if (piece == null) continue;
            if (piece.Side != GameManager.CurrentPlayerTurn) continue;

            //  heuristicValue = cell.Occupant.Side == _owner ? cell.Occupant.HeuristicScore : -cell.Occupant.HeuristicScore;
            if (piece.Side == _owner)
                heuristicValue += piece.Heuristic;
            else
                heuristicValue -= piece.Heuristic;
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

        foreach (Piece piece in _matrix)
        {
            if (piece == null) continue;
            if (piece.Side != OpponentTurn) continue;
            
            // Je dois récupérer le mouvement de chaque Occupant dont c'est le tour et créer un nouveau node basé sur
            // celui du node actuelle et jouer le coup sur celui ci
            // 1 - Récupérer la liste des muvement possible pour cette matrice
            List<Coordinates> moves = piece.AvailableMoves(piece.Coordinates);
            Debug.Log(moves);
            foreach (Coordinates move in moves)
            {
                // 2 - Copier la matrice actuel
                Piece[,] newMatrix = Matrix.DuplicateSnapshot(_matrix);
                
                // 3 - Jouer le mouvement sur la matrice actuel
                Matrix.VirtualPerform(newMatrix, _owner, piece.Coordinates, move);
                
                // 4 - Créer un Node contenant tout les information nécessaire (la nouvelle matrice, les sides)
                Side nextTurn = OpponentTurn == Side.Light ? Side.Dark : Side.Light;
                Node childNode = new Node(OpponentTurn, newMatrix, piece.Coordinates, move);
                

                // 5 - Ajouter le node a la liste des enfants à retourner
                nodeList.Add(childNode);
            }
        }
        return nodeList;
    }
}
