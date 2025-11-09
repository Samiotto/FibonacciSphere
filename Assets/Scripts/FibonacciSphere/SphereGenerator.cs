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
                ConnectPointsWithParastichy(data);
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
            float thetaStep = Mathf.PI * (3f - Mathf.Sqrt(5f)); // golden ratio
            for (int i = 0; i < config.Resolution; i++)
            {
                // place point on the y-axis
                float t = n > 1 ? i / (n - 1f) : 0f; // step size
                float y = Mathf.Lerp(-1f, 1f, t); // place points along the y-axis
                
                // push points out along the x-z plane
                float radius = Mathf.Sqrt(1f - y * y);
                float phi = i * thetaStep; // rotate points by golden ratio
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

        private static void ConnectPointsWithParastichy(SphereData data)
        {
            // --- connect spirals according to each cutoff ---
            for (int c = 0; c < ParastichyConstants.Cutoffs.Length; c++)
            {
                Debug.Log($"Connecting points with ParastichyConstants.Cutoffs[{c}]");
                int previousCutoff = c > 0 ? ParastichyConstants.Cutoffs[c - 1] : 0;
                int nextCutoff = ParastichyConstants.Cutoffs[c];
                
                for (int i = 0; i < nextCutoff; i++)
                {
                    // uncomment this if ParastichyConstants.Intervals is changed to be a 2D array
                    // var intervals = new ArraySegment<int>(ParastichyConstants.Intervals, i * 3, 3); 
                    foreach (var interval in ParastichyConstants.Intervals[c])
                    {
                        for (int j = previousCutoff + interval; j < nextCutoff; j += interval)
                        {
                            if (!data.Connections.ContainsKey(j))
                            {
                                data.Connections.Add(j, new HashSet<int>());
                            }
                            data.Connections[j].Add(i);
                        }
                    }
                }
            }
        }
    }
}
