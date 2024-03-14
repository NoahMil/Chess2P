using Managers;
using UnityEngine;

namespace MinMax
{
    public class AIManager : MonoBehaviour
    {
        [ContextMenu("Think")]
        public void Think()
        {
            Node firstNode = new Node(GameManager.CurrentPlayerTurn, GameManager.CurrentPlayerTurn,
                Matrix.GetCurrentGridSnapshot());
            MinMax(firstNode, 2, true);
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
    }
}