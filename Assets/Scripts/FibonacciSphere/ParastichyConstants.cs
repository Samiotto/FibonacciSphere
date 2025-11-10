using UnityEngine;

namespace FibonacciSphere
{
    public static class ParastichyConstants
    {
        public static int[] Cutoffs = new int[]
        {
            0, 21, 55, 233, 5000
        };
        public static int[][] Intervals = new int[][]
        {
            new int[] {2, 3, 5},
            new int[] {5, 8, 13},
            new int[] {21, 8, 13},
            new int[] {21, 34,13}, 
            new int[] {21, 34, 55}
        };
}

}
