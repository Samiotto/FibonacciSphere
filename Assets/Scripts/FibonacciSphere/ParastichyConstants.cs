using UnityEngine;

namespace FibonacciSphere
{
    public static class ParastichyConstants
    {
        public static int[] Cutoffs = new int[]
        {
            1, 21, 55, 5000 // TODO: 208,000 is approximate, adjust later
        };
        public static int[][] Intervals = new int[][]
        {
            new int[] {2, 3, 5},
            new int[] {5, 8, 13},
            new int[] {21, 8, 13},
            new int[] {21, 34,13} // TODO: random for now, adjust to correct values later
        };
}

}
