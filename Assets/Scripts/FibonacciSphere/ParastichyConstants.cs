/*
 * Author: Sam Garcia
 * cutoffs are being found as every other number in the fibonacci sequence
 * intervals or parastichies are being found by manually by taking prime numbers
 * that produce spirals, then multiplying them by the number of resulting spirals.
 * Every next set of intervals seems to always replace the smallest interval from the
 * previous set, likely because as we reach larger indeces, the smaller intervals get
 * more "stretched" out, and so a larger interval is needed to find closer points.
 *
 * I've been reading through this paper on parastichy patterns in sunflowers and pineapples
 * by Riichirou Negishi, I've specifically been inspired by them to use  the fibonacci numbers used
 * as cutoffs
 * source: https://www.rikuway.org/negi/K377360_C016.pdf
 */

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
