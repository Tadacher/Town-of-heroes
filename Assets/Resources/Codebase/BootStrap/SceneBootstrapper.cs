using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootStrap
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform _spawnPos;
        [SerializeField] private EnemyContainer _enemyContainer;

        private AbstractSoundPlayerService _soundPlayerService;
        private WaveLauncher _waveLauncher;
        private void Awake()
        {
            _enemyContainer.InitializeSelf();
            _soundPlayerService = new SimpleSoundPlayerService(_audioSource);
            _waveLauncher = new(_spawnPos, _enemyContainer, _audioSource);
            _waveLauncher.StartAsync();
        }

        private void OnDisable() => _waveLauncher.CancelRoutine();

    }
}