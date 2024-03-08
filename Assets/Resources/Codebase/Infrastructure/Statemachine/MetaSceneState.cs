namespace Infrastructure
{
    public class MetaSceneState : AbstractSceneState
    {
        private const string _metaSceneName = "MetaGameplayScene";
        private MetaCitySaveLoader _cityLoader;

        public MetaSceneState(MetaCitySaveLoader cityLoader) : base(_metaSceneName)
        {
            _cityLoader = cityLoader;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}
