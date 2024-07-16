using Progress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private Text _woodCount;
    [SerializeField] private Text _stoneCount;
    [SerializeField] private Text _foodCount;
    [SerializeField] private Text _scrollCount;

    private ResourceService _resourceService;

    [Inject]
    public void Initialize(ResourceService resourceService)
    {
        _resourceService = resourceService;

        resourceService.OnStoneGathered += SetStone;
        resourceService.OnWoodGathered += SetWood;
        resourceService.OnFoodGathered += SetFood;
        resourceService.OnScrollsGathered += SetScrolls;
        Refresh();
    }


    private void Refresh() => _resourceService.Refresh();

    private void SetScrolls(int scrolls)
    {
        _scrollCount.text = scrolls.ToString();
    }

    private void SetWood(int wood) => _woodCount.text = wood.ToString();
    private void SetStone(int stone) => _stoneCount.text = stone.ToString();
    private void SetFood(int food) => _foodCount.text = food.ToString();

    private void OnDestroy()
    {
        _resourceService.OnStoneGathered -= SetStone;
        _resourceService.OnWoodGathered -= SetWood;
        _resourceService.OnFoodGathered -= SetFood;
        _resourceService.OnScrollsGathered -= SetScrolls;
    }
}
