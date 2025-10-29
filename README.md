# Fibonacci Sphere – Project Notes

## Summary

Module for generating a fibonacci Sphere, works by placing points along an axis, then pushing them away from the axis on a perpendicular plane, rotating by the golden ratio (3 - sqrt(5)) every push. This method generates a sphere with very evenly spaced points. When finished, will link nearby points together, creating a mesh in which a texture can be drawn.

## Requirements

- Accept `radius`, `resolution`, and optional `seed` parameters (via serialized fields or constructor) to build the Fibonacci sphere.
- `seed` parameter will be used to add random height variance and random features to the sphere's surface.
- Generate surface points using the parameters and expose a way to export the point collection so the system can act as a standalone model.
- Create a linking system that connects surface nodes for mesh construction; linking will occur whether or not a mesh is drawn.
- Drawing the texture mesh is optional so that external callers can provide their own rendering.
- Visualize the sphere both as points floating in space and via node connections/edges.
