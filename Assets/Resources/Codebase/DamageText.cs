using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMesh;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    public void Initialize(int damage)
    {
        _textMesh.text = damage.ToString();
        StartCoroutine(Move(_lifeTime));
    }

    private IEnumerator Move(float lifetime)
    {
        while (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            transform.position += _speed * Time.deltaTime * Vector3.up;
            yield return null;
        }
        ReturnToPool();
    }
    private void ReturnToPool() => gameObject.SetActive(false);
} 
