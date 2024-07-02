using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Mathematics;
using TMPro;

namespace Cuebat.Algorithms.ShortestPath
{
    public class ShortestPath_BruteForce : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("How many searches should be done each time before returning the control to the engine")]
        int delayCount = 10000;
        public Transform[] locations;

        public int[] shortestPathLocations;

        public float time = 0;
        public int searchCount = 1;
        float shortestPathLength = float.MaxValue;
        public int[] indices;

        public async void RunSimulation()
        {
            indices = new int[locations.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }
            await GeneratePermutations(indices, 0, locations.Length - 2);
        }

        public async Task GeneratePermutations(int[] indices, int Start, int end)
        {
            if (Start == end)
            {
                await ExecuteForArray(indices);
            }
            else
            {

                for (int i = Start; i <= end; i++)
                {
                    Swap(ref indices[Start], ref indices[i]);
                    await GeneratePermutations(indices, Start + 1, end);
                    Swap(ref indices[Start], ref indices[i]);
                }
            }
        }

        void Swap(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }

        int currentDelayCount = 0;
        public async Task ExecuteForArray(int[] transforms)
        {
            if (transforms[0] > transforms[^2])
                return;

            searchCount++;

            float Length = 0;
            for (int i = 0; i < transforms.Length - 1; i++)
            {
                Length += math.distancesq(locations[transforms[i]].position, locations[transforms[i + 1]].position);
            }

            Length += math.distancesq(locations[transforms[0]].position, locations[transforms[^1]].position);

            if (Length < shortestPathLength)
            {
                shortestPathLength = Length;
                shortestPathLocations = (int[])transforms.Clone();
                await Task.Delay(1);
                time += 1;
            }
            currentDelayCount++;
            if (currentDelayCount > delayCount)
            {
                await Task.Delay(1);
                time += 1;
                currentDelayCount = 0;
            }
        }

    }
}