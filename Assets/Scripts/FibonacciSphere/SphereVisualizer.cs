using System;
using UnityEngine;

namespace FibonacciSphere
{
    public sealed class SphereVisualizer : MonoBehaviour
    {
        [SerializeField] private float pointSize = 0.05f;
        [SerializeField, Range(1,10)] private int interval = 3;

        //private SphereData _data;
        
        public void Draw(SphereData data)
        {
            // store latest SphereData
            //_data = data;
            
            Gizmos.color = Color.white;
            int i = 0;
            foreach (var point in data.Points)
            {
                if (i % interval == 0) Gizmos.color = Color.red;
                else Gizmos.color = Color.white;
                Gizmos.DrawSphere(point, pointSize);
                i++;
            }
        }

        private void OnValidate()
        {
            //Draw(_data);
        }
    }
}
