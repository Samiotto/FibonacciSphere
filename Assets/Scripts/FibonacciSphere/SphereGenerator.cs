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
                if (p * 2 > fibonacciPattern.Count)
                {
                    Debug.LogWarning($"trying to access fibonacci number outside of calculated range. p: {p}, p*2: {p * 2}, fibonacciPattern.Count: {fibonacciPattern.Count}");
                }

                int lowerIndex = (p * 2) - 6;
                if (lowerIndex < 0) lowerIndex = 0;
                for (int i = fibonacciPattern[lowerIndex]; i < fibonacciPattern[p * 2] && i < pointCount; i++)
                {
                    if (i + p > pointCount)
                    {
                        Debug.Log("i + p is greater than pointCount");
                        Debug.Log($"i: {i} p: {p}");
                        break;
                    }

                    if (i + fibonacciPattern[p] >= pointCount) break;
                    data.Connections[i].Add(fibonacciPattern[p] + i);
                }
            }
        }
    }
}
