using System.Collections;
using UnityEngine;

namespace Infrastructure
{

    public interface ICoroutineRunner
    {
        public UnityEngine.Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}
