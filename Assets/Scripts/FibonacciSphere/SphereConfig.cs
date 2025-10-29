namespace FibonacciSphere
{
    public struct SphereConfig
    {
        public float Radius;
        public int Resolution;
        public int Seed;

        /// <summary>
        /// Stores the configuration data for generating a sphere.
        /// </summary>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="resolution">Number of points on the sphere surface</param>
        /// <param name="seed">seed for adding variance to the shape, currently unused</param>
        public SphereConfig(float radius, int resolution, int seed)
        {
            Radius = radius;
            Resolution = resolution;
            Seed = seed;
        }
    }
}

