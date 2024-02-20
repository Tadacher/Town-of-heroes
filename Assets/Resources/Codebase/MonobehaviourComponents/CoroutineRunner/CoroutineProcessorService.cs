using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class CoroutineProcessorService : MonoBehaviour, ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator routine) => StartCoroutine(routine);
        void ICoroutineRunner.StopCoroutine(UnityEngine.Coroutine coroutine) => StopCoroutine(coroutine);
    }
}