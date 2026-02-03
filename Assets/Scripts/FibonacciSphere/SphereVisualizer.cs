using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FibonacciSphere
{
    public sealed class SphereVisualizer : MonoBehaviour
    {
        [SerializeField] private float pointSize = 0.05f;
        [SerializeField] private int highlightToIndex = 0;
        [SerializeField] private int[] intervals = new int[3];
        [SerializeField] private int[] residuals = new int[3];
        [SerializeField] private float[] minDistances = new float[3];

        //private SphereData _data;
        
        public void Draw(SphereData data)
        {
            // store latest SphereData
            //_data = data;

            // ensure intervals is at least 3 long
            if (intervals.Length != 3)
            {
                int[] temp = new int[3];
                for (int j = 0; j < temp.Length; j++) temp[j] = 1;
                Array.Copy(intervals, temp, intervals.Length);
                intervals = temp;
            }

            for (int j = 0; j < minDistances.Length; j++)
            {
                minDistances[j] = 2f;
            }
            
            Gizmos.color = Color.black;
            int i = 0;
            Vector3[] previousPrimeMultiplePoint = {Vector3.zero, Vector3.zero, Vector3.zero};
            int[] previousSmallestDistancePointIndex = {0, 0, 0};
            foreach (var point in data.Points)
            {
                Gizmos.color = Color.black;

                for (int j = 0; j < 3; j++)
                {
                    if (intervals[j] > 0 && i % intervals[j] == 0)
                    {
                        if (j == 0) Gizmos.color += Color.red;
                        else if (j == 1) Gizmos.color += Color.green;
                        else if (j == 2) Gizmos.color += Color.blue;
                        Gizmos.DrawLine(previousPrimeMultiplePoint[j], point);
                        if (minDistances[j] > Vector3.Distance(data.Points[previousSmallestDistancePointIndex[j]], point))
                        {
                            minDistances[j] = Vector3.Distance(data.Points[previousSmallestDistancePointIndex[j]], point);
                            residuals[j] = i - previousSmallestDistancePointIndex[j];
                        }
                        previousPrimeMultiplePoint[j] = point;
                        previousSmallestDistancePointIndex[j] = i;
                    }
                }
                
                if (i < highlightToIndex) Gizmos.color = Color.darkCyan;
                if (i == highlightToIndex) Gizmos.color = Color.aquamarine;
                Gizmos.DrawSphere(point, pointSize);
                
                // draw connections
                Gizmos.color = Color.darkCyan;
                if (data.Connections.ContainsKey(i))
                {
                    foreach (var connection in data.Connections[i])
                    {
                        Gizmos.DrawLine(data.Points[connection], point);
                    }
                }
                
                i++;
            }
        }

        
    }
}
