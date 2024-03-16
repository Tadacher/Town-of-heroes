using Progress;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private Text _woodCount;
    [SerializeField] private Text _stoneCount;
    [SerializeField] private Text _foodCount;

    private ResourceService _service;

    [Inject]
    public void Initialize(ResourceService resourceService)
    {
        _service = resourceService;

        resourceService.OnStoneGathered += SetStone;
        resourceService.OnWoodGathered += SetWood;
        resourceService.OnFoodGathered += SetFood;
        Refresh();
    }

    private void Refresh() => _service.Refresh();

    private void SetWood(int wood) => _woodCount.text = wood.ToString();
    private void SetStone(int stone) => _stoneCount.text = stone.ToString();
    private void SetFood(int food) => _foodCount.text = food.ToString();

    private void OnDestroy()
    {
        _service.OnStoneGathered -= SetStone;
        _service.OnWoodGathered -= SetWood;
        _service.OnFoodGathered -= SetFood;
    }
}
