using UnityEngine;

namespace FibonacciSphere
{
    public static class ParastichyConstants
    {
        public static int[] Cutoffs = new int[]
        {
            44, 710, 208_000 // TODO: 208,000 is approximate, adjust later
        };
        public static int[][] Intervals = new int[3][]
        {
            new int[] {2, 3, 5},
            new int[] {21,34,55},
            new int[] {100,150,225} // TODO: random for now, adjust to correct values later
        };
}

}
