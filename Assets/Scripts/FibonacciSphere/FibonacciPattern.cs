using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FibonacciSphere
{
    public class FibonacciPattern
    {
        private int[] _startingSequence = new[] { 1, 1 };

        private static Dictionary<int, int> _sequence;
        
        public int this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new System.IndexOutOfRangeException();
                } 
                else if (index >= _sequence.Count)
                {
                    for (int i = _sequence.Count; i <= index; i++)
                    {
                        ComputeOne();
                    }
                }
                return _sequence[index];
            }
        }
        
        public int Count => _sequence.Count;

        public FibonacciPattern()
        {
            _sequence = new Dictionary<int, int>();
            _sequence.Add(0, 1);
            _sequence.Add(1, 1);
        }

        public void GenerateUntilGreaterThan(int valueToBeat)
        {
            while (_sequence.TryGetValue(_sequence.Count - 1, out int value) && 
                   value < valueToBeat)
            {
                ComputeOne();
            }
        }

        private void ComputeOne()
        {
            if (_sequence.ContainsKey(_sequence.Count - 1) && _sequence.ContainsKey(_sequence.Count - 2))
            {
                _sequence.TryGetValue(_sequence.Count - 1, out var value1);
                _sequence.TryGetValue(_sequence.Count - 2, out var value2);
                _sequence.Add(_sequence.Count, value1 + value2);
            }
        }

        
        public new String ToString()
        {
            String output = String.Empty;
            for (int i = 0; i < _sequence.Count; i++)
            {
                _sequence.TryGetValue(i, out var value);
                output += value + ", ";
            }
            return output;
        }


    }
}
