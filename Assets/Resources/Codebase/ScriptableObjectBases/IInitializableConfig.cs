/// <summary>
/// Interface for self-initializable congifs
/// ScriptableObject must inherit this to be initialized at binding stage
/// </summary>
public interface IInitializableConfig
{
    public void Initialize();
}