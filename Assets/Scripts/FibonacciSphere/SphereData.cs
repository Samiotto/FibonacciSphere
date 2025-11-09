using System.Collections.Generic;
using UnityEngine;

namespace FibonacciSphere
{
    public sealed class SphereData
    {
        public Vector3[] Points { get; private set; }
        public Dictionary<int, HashSet<int>> Connections { get; private set; }

        public int Count => Points?.Length ?? 0;

        public SphereData(Vector3[] points)
        {
            Points = points;
            Connections = new Dictionary<int, HashSet<int>>();
        }
        
        
    }
}
