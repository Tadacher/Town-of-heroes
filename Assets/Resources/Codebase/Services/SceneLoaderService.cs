using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoaderService
    {
        ICoroutineRunner _coroutineProcessor;

        public SceneLoaderService(ICoroutineRunner coroutineProcessor) => 
            _coroutineProcessor = coroutineProcessor;

        public void StartLoadSceneCoroutine(string name, Action onloaded = null) => 
            _coroutineProcessor.StartCoroutine(LoadSceneAsync(name, onloaded));
       
        private IEnumerator LoadSceneAsync(string nextSceneName, Action onloaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextSceneName)
            {
                onloaded?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);

            while(!asyncOperation.isDone)
            {
                yield return null;
            }
            onloaded?.Invoke();
        }
    }
}
