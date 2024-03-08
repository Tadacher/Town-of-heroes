using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Metagameplay.Ui
{
    public class MetaUiContainer : MonoBehaviour
    {
        [Header("GeneralUi")]
        public GameObject GeneralUi;
        public Button BuildMenuBtn;

        [Header("BuildMenuUi")]
        public BuildMenuUiContainer BuildMenuUi;
    }
}
