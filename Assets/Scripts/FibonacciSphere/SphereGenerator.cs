using UnityEngine;

public class SphereGenerator
{
    [Header("Sphere Parameters")]
    [SerializeField] private int _resolution = 100;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private readonly int _seed;

    public SphereGenerator(int resolution, float radius, int seed)
    {
        _resolution = resolution;
        _radius = radius;
        _seed = seed;
    }

    public SphereGenerator(int resolution, float radius) 
        : this(resolution, radius, Random.Range(0, 1_000_000))
    {}
    private SphereNode[] GenerateSpherePoints()
    {
        
        
        throw new System.NotImplementedException();
    }
}
