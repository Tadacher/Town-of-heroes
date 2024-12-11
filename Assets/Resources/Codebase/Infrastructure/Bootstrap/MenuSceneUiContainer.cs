using UnityEngine;
using UnityEngine.UI;

public class MenuSceneUiContainer : MonoBehaviour
{
    public Button StartButton => _startButton;

    [SerializeField] private Button _startButton;

}