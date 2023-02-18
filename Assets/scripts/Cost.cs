using UnityEngine;
class Cost : MonoBehaviour
{
    [SerializeField] int swordCount;
    [SerializeField] float swordPrice;
    [SerializeField] float totalGold;

    private void Start()
    {
        totalGold = swordCount * swordPrice;
        Debug.Log(totalGold);
    }
}

