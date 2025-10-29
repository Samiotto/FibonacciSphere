using System;
using UnityEngine;
using FibonacciSphere;

public class SphereRunner : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField, Min(1)] private int resolution = 100;
    [SerializeField] private int seed = 0;
    [SerializeField] private SphereVisualizer _sphereVisualizer;

    private SphereConfig SphereConfig => new SphereConfig(radius, resolution, seed);
    private SphereData _data;

    private void Awake()
    {
        _data = SphereGenerator.Generate(SphereConfig);
    }
    
    private void OnValidate()
    {
        _data = SphereGenerator.Generate(SphereConfig);
    }

    void OnDrawGizmos()
    {
        if (_sphereVisualizer != null && _data != null)
        {
            _sphereVisualizer.Draw(_data);
        }
    }

    
}
