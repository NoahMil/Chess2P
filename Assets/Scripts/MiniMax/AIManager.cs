using System;
using UnityEngine;

using Data;
using Managers;

namespace MinMax
{
    public class AIManager : MonoBehaviour
    {
        /*
        private int _maxEval;
        
        [ContextMenu("Think")]
        public void Think()
        {
            Node firstNode = new Node(GameManager.CurrentPlayerTurn, GameManager.CurrentPlayerTurn, Matrix.GetCurrentGridSnapshot());
            _maxEval = MinMax(firstNode, 2, true);

            foreach (Node node in firstNode.GetChilds())
            {
                
            }
        }

        public int MinMax(Node node, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || node.IsTerminal())
            {
                return node.GetHeuristicValue();
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Node child in node.GetChilds())
                {
                    maxEval = Mathf.Max(maxEval, MinMax(child, depth - 1, false));
                }

                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Node child in node.GetChilds())
                {
                    minEval = Mathf.Min(minEval, MinMax(child, depth - 1, true));
                }

                return minEval;
            }
        }

        private void Update()
        {
            Debug.Log(_maxEval);
        }
        */
    }
    
}