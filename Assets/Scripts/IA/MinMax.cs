using System;
using UnityEngine;

using Enums;

namespace IA
{
    public static class Algorythm
    {
        public static float MinMax(Node node, int depth, bool maximazingPlayer)
        {
            float value;
            
            if (depth == 0 || node.IsTerminal())
                return node.Heuristic;
            
            if (maximazingPlayer) {
                value = Mathf.NegativeInfinity;
                foreach (Node child in node.DiscoverChild())
                    value = Mathf.Max(value, MinMax(child, depth - 1, false));
            }
            else {
                value = Mathf.Infinity;
                foreach (Node child in node.DiscoverChild())
                    value = Mathf.Min(value, MinMax(child, depth - 1, true));
            }

            return value;
        }
    }
}
