using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FibonacciSphere
{
    public static class SphereGenerator
    {
        public static SphereConfig SphereConfig;

        public enum ConnectionMethod
        {
            Delaunay,
            Parastichy // PrimeIntervals
        }
        
        /// <summary>
        /// Generate a fibonacci sphere with the given config.
        /// </summary>
        /// <param name="config">configuration data for the sphere, including radius and number of points</param>
        /// <param name="connectionMethod">method used for linking points on the sphere</param>
        /// <returns></returns>
        public static SphereData Generate(SphereConfig config, ConnectionMethod connectionMethod = ConnectionMethod.Parastichy)
        {
            SphereData data = new SphereData(GeneratePointsOfFibonacciSphere(config));

            if (connectionMethod == ConnectionMethod.Delaunay)
            {
                // ConnectPointsWithDelaunay(ref points);
            }
            else if (connectionMethod == ConnectionMethod.Parastichy)
            {
                // ConnectPointsWithParastichy(data);
                // ConnectPointsWithParastichyMovingWindow(data);
                ConnectPointsUsingParastichyPatterns(data);
            }
            
            
            return data;
        }

        /// <summary>
        /// Generate a fibonacci sphere with the given radius, resolution, and seed.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="resolution"></param>
        /// <param name="seed">used if randomizing </param>
        /// <returns></returns>
        public static SphereData Generate(float radius, int resolution, int seed)
        {
            return Generate(new SphereConfig(radius, resolution, seed));
        }

        /// <summary>
        /// Constructor using a random seed
        /// </summary>
        /// <param name="radius">the end radius of the sphere after generating</param>
        /// <param name="resolution">number of points on the sphere</param>
        /// <returns></returns>
        public static SphereData Generate(float radius, int resolution)
        {
            int seed = UnityEngine.Random.Range(0, int.MaxValue);
            return Generate(radius, resolution, seed);
        }

        private static Vector3[] GeneratePointsOfFibonacciSphere(SphereConfig config)
        {
            int n = Mathf.Max(0, config.Resolution); // ensure n is positive
            Vector3[] points = new Vector3[config.Resolution];
            
            // generate points
            float thetaStep = Mathf.PI * (3f - Mathf.Sqrt(5f)); // golden angle
            for (int i = 0; i < config.Resolution; i++)
            {
                // place point on the y-axis
                float t = n > 1 ? i / (n - 1f) : 0f; // step size
                float y = Mathf.Lerp(-1f, 1f, t); // place points along the y-axis
                
                // push points out along the x-z plane
                float radius = Mathf.Sqrt(1f - y * y);
                float phi = i * thetaStep; // rotate points by golden angle
                points[i].x = radius * Mathf.Cos(phi);
                points[i].z = radius * Mathf.Sin(phi);
                points[i].y = y;
            }
            
            return points;
        }

        private static Vector3[] ConnectPointsWithDelaunay(Vector3[] points)
        {
            // --- stereographic projection with delaunay triangulation ---
            // project points onto a plane
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Vector2[] projectedPoints = ProjectPointsToPlane(points, plane);
            
                
            // use delaunay triangulation to connect points
                
                
                
            // project points back onto the sphere
            
            throw new NotImplementedException();
        }

        private static Vector2[] ProjectPointsToPlane(Vector3[] spherePoints, Plane plane)
        {
            Vector2[] projectedPoints = new Vector2[spherePoints.Length];
            for (int i = 0; i < spherePoints.Length; i++)
            {
                projectedPoints[i] = ProjectPointToPlane(spherePoints[i], plane);
            }
            return projectedPoints;
        }

        private static Vector2 ProjectPointToPlane(Vector3 point, Plane plane, float epsilon = 1e-7f)
        {
            float denominator = 1f - point.y;
            if (Mathf.Abs(denominator) < epsilon)
            {
                // approching infinity (denominator is almost 0) use epsilon instead
                Vector2 direction = new Vector2(point.x, point.z).normalized;
                return direction * (1f / epsilon);
            }
            
            return new Vector2(point.x, point.z) / denominator;
        }

        private static void ConnectPointsUsingParastichyPatterns(SphereData data)
        {
            ConnectPointsOneParastichyAtATime(data);
            // ConnectPointsWithParastichyMovingWindow(data);
        }

        private static void ConnectPointsOneParastichyAtATime(SphereData data)
        {
            int pointCount = data.Count;

            // Ensure every point index has an entry to prevent key lookups from failing
            for (int i = 0; i < pointCount; i++)
            {
                if (!data.Connections.ContainsKey(i))
                {
                    data.Connections[i] = new HashSet<int>();
                }
            }
            
            // generate all necessary numbers in the fibonacci pattern
            FibonacciPattern fibonacciPattern = new FibonacciPattern();
            Debug.Log($"Starting values in fibonacci {fibonacciPattern.ToString()}");
            fibonacciPattern.GenerateUntilGreaterThan(pointCount);
            Debug.Log($"Fibonacci sequence computed past {pointCount} {fibonacciPattern.ToString()}");
            int nextNumber = fibonacciPattern[fibonacciPattern.Count];
            Debug.Log($"Next in the sequence: {nextNumber}");
            
            // calculate number of parastichies needed
            int numParastichies = 0;
            while (pointCount > fibonacciPattern[numParastichies * 2])
            {
                numParastichies++;
            }

            numParastichies += 2;
            
            Debug.Log($"Number of parastichies {numParastichies}");
            
            // loop through every parastichy, connect all points that are that parastichy away within its interval
            for (int p = 0; p <= numParastichies; p++)
            {
                Debug.Log($"Working through parastichy {numParastichies} which is a value of {fibonacciPattern[p]}");
                for (int i = p * 2 - 6; p < p * 2; p++)
                {
                    Debug.Log($"i: {i}");
                    if (i < 0) continue;
                    if (i + p > pointCount) break;
                    data.Connections[i].Add(fibonacciPattern[p] + i);
                }
            }
        }

        private static void ConnectPointsWithParastichyMovingWindow(SphereData data)
        {
            int pointCount = data.Count;

            // Ensure every point index has an entry to prevent key lookups from failing
            for (int i = 0; i < pointCount; i++)
            {
                if (!data.Connections.ContainsKey(i))
                {
                    data.Connections[i] = new HashSet<int>();
                }
            }
            
            // generate all necessary numbers in the fibonacci pattern
            FibonacciPattern fibonacciPattern = new FibonacciPattern();
            Debug.Log($"Starting values in fibonacci {fibonacciPattern.ToString()}");
            fibonacciPattern.GenerateUntilGreaterThan(pointCount);
            Debug.Log($"Fibonacci sequence computed past {pointCount} {fibonacciPattern.ToString()}");
            int nextNumber = fibonacciPattern[fibonacciPattern.Count];
            Debug.Log($"Next in the sequence: {nextNumber}");

            // calculate the number of intervals needed
            int numIntervals;
            for (numIntervals = 0; numIntervals < fibonacciPattern.Count; numIntervals++)
            {
                if (pointCount < fibonacciPattern[(numIntervals+1)*2]) break;
            }
            
            Debug.Log($"Number of intervals: {numIntervals}");
            Debug.Log($"Last end of interval should be {fibonacciPattern[(numIntervals+1)*2]}");
            // loop through every interval
            for (int interval = 0; interval < numIntervals; interval++)
            {
                Debug.Log($"Starting interval {interval}");
                // every interval starts and ends between every other point in the fibonacci sequence
                // (ex. start 0, skip 1, end 1, then start 1, skip 2, end 3, then start 3, skip 5, end 8)
                int start = fibonacciPattern[(interval) * 2];
                int end = fibonacciPattern[(interval + 1) * 2];
                
                Debug.Log($"Interval starts at {start} and ends at {end}");
                Debug.Log($"We will use parastichies {fibonacciPattern[interval + 1]}, {fibonacciPattern[interval + 2]}, and {fibonacciPattern[interval + 3]}");

                int CONNECTIONS_MADE = 0;
                // connect points using a moving window of parastichies based on fibonacci sequence
                // (i.e. 1,1,2 then 1,2,3 then 2,3,5 etc.) while looping through every point in the interval
                for (int i = start; i <= end && i + interval + 1 < pointCount; i++)
                {
                    data.Connections[i].Add(fibonacciPattern[i + interval + 1]);
                    CONNECTIONS_MADE++;
                }
                for (int i = start; i <= end && i + interval + 2 < pointCount; i++)
                {
                    data.Connections[i].Add(fibonacciPattern[i + interval + 2]);
                    CONNECTIONS_MADE++;
                }
                for (int i = start; i <= end && i + interval + 3 < pointCount; i++)
                {
                    data.Connections[i].Add(fibonacciPattern[i + interval + 3]);
                    CONNECTIONS_MADE++;
                }
                Debug.Log($"Made {CONNECTIONS_MADE} connections");
            }
        }

        private static void ConnectPointsWithParastichy(SphereData data)
        {
            int pointCount = data.Count;

            // Ensure every point index has an entry to prevent key lookups from failing
            for (int i = 0; i < pointCount; i++)
            {
                if (!data.Connections.ContainsKey(i))
                {
                    data.Connections[i] = new HashSet<int>();
                }
            }
            
            // for every cutoff range
            for (int rangeStartIndex = 0; rangeStartIndex < ParastichyConstants.Cutoffs.Length; rangeStartIndex++)
            {
                
            }

            // --- connect spirals according to each cutoff ---
            for (int c = 0; c < ParastichyConstants.Cutoffs.Length; c++)
            {
                // Safety: guard against mismatched intervals array
                if (c >= ParastichyConstants.Intervals.Length) break;

                // Debug.Log($"Connecting points with ParastichyConstants.Cutoffs[{c}]");
                int previousCutoff = c > 0 ? ParastichyConstants.Cutoffs[c - 1] : 0;
                int nextCutoff = ParastichyConstants.Cutoffs[c];

                // Clamp to actual number of points
                int rangeStart = Mathf.Clamp(previousCutoff, 0, pointCount);
                int rangeEnd = Mathf.Clamp(nextCutoff, 0, pointCount);
                if (rangeStart >= rangeEnd) continue;

                foreach (var interval in ParastichyConstants.Intervals[c])
                {
                    if (interval <= 0) continue; // ignore invalid intervals

                    // For each residue class r in [0, interval), build its chain within the range
                    for (int r = 0; r < interval; r++)
                    {
                        // First index in [rangeStart, rangeEnd) with idx % interval == r
                        int offset = (r - (rangeStart % interval) + interval) % interval;
                        int first = rangeStart + r;
                        int prev = first;
                        first += interval;
                        if (first >= rangeEnd) break;
                        
                        for (int idx = prev; idx < Mathf.Clamp(rangeEnd + interval, 0, pointCount); idx += interval)
                        {
                            if (prev >= rangeStart)
                            {
                                // Store neighbor under the current index so the visualizer can draw
                                data.Connections[idx].Add(prev);
                                // If bidirectional edges are desired, also connect the previous to current:
                                data.Connections[prev].Add(idx);
                            }
                            prev = idx;
                        }
                    }
                }
            }
        }
    }
}
