using UnityEngine;

[CreateAssetMenu(fileName = "SphereNode", menuName = "Scriptable Objects/SphereNode")]
public class SphereNode : ScriptableObject
{
        private Vector3 _position;
        private readonly Vector3 _originalPosition; 
        private static Vector3 _origin = Vector3.zero;
        private SphereNode[] _neighbors;
        
        public Vector3 Position => _position;
        
        public SphereNode(Vector3 position)
        {
            _originalPosition = position;
            _position = position;
        }
        
        public void AdjustPositionVertical(float amount)
        {
            // adjust away or torwards origin
            Vector3 direction = _position - _origin;
            // direction.y = 0;
            _position = _origin + direction.normalized * amount;
        }
    
}
