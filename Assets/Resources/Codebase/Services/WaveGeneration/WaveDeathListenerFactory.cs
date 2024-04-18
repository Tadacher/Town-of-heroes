using Services.CardGeneration;
using Services.Factories;
using UnityEngine;
using Zenject;

public class WaveDeathListenerFactory : AbstractPoolerFactory<WaveDeathListener>
{
    private readonly CardGenerationService _cardGenerationService;

    public WaveDeathListenerFactory(DiContainer diContainer, CardGenerationService cardGenerationService) : base(diContainer)
    {
        _cardGenerationService = cardGenerationService;
    }

    protected override void ActionOnDestroy(WaveDeathListener poolable)
    {
        poolable.OnMobDeath -= GenerateMapWithChance;
    }

    protected override void ActionOnGet(WaveDeathListener poolable)
    {
    }

    protected override void ActionOnRelease(WaveDeathListener type)
    {
    }

    protected override WaveDeathListener CreateNew()
    {
        var listener = new WaveDeathListener(this);
        listener.OnMobDeath += GenerateMapWithChance;
        return listener;
    }

    private void GenerateMapWithChance()
    {
        Debug.Log("MobIsDead");
        GenerateMapWithChance(0.1f);
    }

    private void GenerateMapWithChance(float chance)
    {
        if (UnityEngine.Random.Range(0f, 1f) < chance)
        {
            Debug.Log("draft");
            _cardGenerationService.DraftCard();
        }
    }
}
