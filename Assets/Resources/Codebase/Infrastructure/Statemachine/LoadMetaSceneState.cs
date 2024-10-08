﻿using Infrastructure;
using System;

namespace Infrastructure
{
    public class LoadMetaSceneState : IPayloadedState<string>
    {
        private AbstractStateMachine _gameStateMachine;
        private SceneLoaderService _sceneLoaderService;

        public LoadMetaSceneState(SceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            return this;
        }

        void IPayloadedState<string>.Enter(string payload) => 
            _sceneLoaderService.StartLoadSceneCoroutine(payload, EnterLevelState);

        private void EnterLevelState() => _gameStateMachine.EnterState<MetaSceneState>();

        void IExitableState.Exit()
        {

        }

    }
}
