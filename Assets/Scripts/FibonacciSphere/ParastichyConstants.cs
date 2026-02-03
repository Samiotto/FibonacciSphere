/*
 * Author: Sam Garcia
 * cutoffs are being found as every other number in the fibonacci sequence
 * intervals or parastichies are being found by manually by taking prime numbers
 * that produce spirals, then multiplying them by the number of resulting spirals.
 * Every next set of intervals seems to always replace the smallest interval from the
 * previous set, likely because as we reach larger indeces, the smaller intervals get
 * more "stretched" out, and so a larger interval is needed to find closer points.
 *
 * Ok before I forget I need to record this, I just realized that I was thinking about Parastichies/Intervals
 * in a way more complicated way than I needed to. the parastichies are literally just the fibonacci numbers.
 * I also think this needs to be rewritten so that instead of using sets of 3 intervals, we instead use a moving
 * window where we apply the parastichies accross 3 cutoff ranges at a time, this might help with some of
 * the cutoff boundaries where some connections appear to get skipped.
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
            0, 1, 3, 8, 21, 55, 233, 5000
        };
        public static int[][] Intervals = new int[][]
        {
            new int[] {1, 1, 2},
            new int[] {1, 2, 3},
            new int[] {2, 3, 5},
            new int[] {3, 5, 8},
            new int[] {5, 8, 13},
            new int[] {21, 8, 13},
            new int[] {21, 34,13}, 
            new int[] {21, 34, 55}
        };

        public static int[] FibonacciSequence = new int[]
        {
            0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711
        };
    }

}
