using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Services
{
    public class DamageTextService
    {
        private List<DamageText> _objectPool;
        private DamageText _prefab;

        private const float _delay = 1f;
        public DamageTextService()
        {
            _objectPool = new();
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Text/DamageText");
            _prefab = prefab.GetComponent<DamageText>();
            Debug.Log(_prefab);
        }
        public DamageText ReturnDamageText(int damage, Vector3 pos)
        {
            foreach (DamageText damageText in _objectPool)
            {
                if (!damageText.gameObject.activeSelf)
                {
                    damageText.gameObject.SetActive(true);

                    damageText.Initialize(damage);
                    damageText.transform.position = pos + RandomOffset();

                    return damageText;
                }
            }
            DamageText newProjectileBehaviour = GameObject.Instantiate(_prefab, pos + RandomOffset(), Quaternion.identity);
            _objectPool.Add(newProjectileBehaviour);
            newProjectileBehaviour.Initialize(damage);
            return newProjectileBehaviour;
        }

        private Vector3 RandomOffset() => new Vector3(UnityEngine.Random.Range(-1*_delay, _delay), UnityEngine.Random.Range(-1*_delay, _delay), 0);
    }
}