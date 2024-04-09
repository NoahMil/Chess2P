using System;
using Data;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniMax
{
    public class AIManager : MonoBehaviour
    {
        private float _maxEval;
        [SerializeField] private GameManager _gameManager;

        
        [ContextMenu("Think")]
        public void Think()
        {
            Node firstNode = new Node(GameManager.CurrentPlayerTurn, Matrix.GetCurrentGridSnapshot(),new Coordinates(-1,-1),new Coordinates(-1,-1));
            Node bestChild = null;
         
            foreach (Node child in firstNode.GetChilds()) 
            {
                float score = MinMax(firstNode, 1, false);
                if (bestChild == null || score > bestChild.GetHeuristicValue()) {
                    bestChild = child;
                }
                else if (score == bestChild.GetHeuristicValue() && Random.Range(0,2f) == 0)
                {
                    bestChild = child;
                }
            
                _gameManager.PerformMovement(bestChild.Origin,bestChild.Destination);
            }
            MinMax(firstNode, 2, true);
        }
        
        

        public float MinMax(Node node, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || node.IsTerminal())
            {
                return node.GetHeuristicValue();
            }

            if (maximizingPlayer)
            {
                float maxEval = int.MinValue;
                foreach (Node child in node.GetChilds())
                {
                    maxEval = Mathf.Max(maxEval, MinMax(child, depth - 1, false));
                }

                return maxEval;
            }
            else
            {
                float minEval = int.MaxValue;
                foreach (Node child in node.GetChilds())
                {
                    minEval = Mathf.Min(minEval, MinMax(child, depth - 1, true));
                }

                return minEval;
            }
        }
    }
}