using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class CoroutineProcessorService : MonoBehaviour, ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator routine)
        {
           return StartCoroutine(routine);
        }
    }
}