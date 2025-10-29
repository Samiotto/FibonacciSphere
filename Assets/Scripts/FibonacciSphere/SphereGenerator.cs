using System;
using UnityEngine;

namespace FibonacciSphere
{
    public static class SphereGenerator
    {
        public static SphereConfig SphereConfig;
        
        /// <summary>
        /// Generate a fibonacci sphere with the given config.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SphereData Generate(SphereConfig config)
        {
            Vector3[] points = new Vector3[config.Resolution];
            
            // place points along the y-axis
            float startY = -1;
            float step = 2f / config.Resolution;
            for (int i = 0; i < config.Resolution; i++)
            {
                points[i].y = startY + step * i;
            }
            
            // push points out along the x-z plane, rotating by golden ratio
            float thetaStep = Mathf.PI * (3f - Mathf.Sqrt(5f));
            for (int i = 0; i < config.Resolution; i++)
            {
                float radius = Mathf.Sqrt(1f - points[i].y * points[i].y);
                float phi = i * thetaStep;
                points[i].x = radius * Mathf.Cos(phi);
                points[i].z = radius * Mathf.Sin(phi);
            }
            
            return new SphereData(points);
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
    }
}
