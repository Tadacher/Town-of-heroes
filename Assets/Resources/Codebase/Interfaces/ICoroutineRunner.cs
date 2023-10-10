using System.Collections;

namespace Infrastructure
{

    public interface ICoroutineRunner
    {
        public UnityEngine.Coroutine StartCoroutine(IEnumerator enumerator);
    }
}
