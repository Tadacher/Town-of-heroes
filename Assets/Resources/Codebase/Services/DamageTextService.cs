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

            if (prefab == null)
            {
                Debug.Log("No damage text refab found at Prefabs/Text/DamageText");
                return;
            }
            _prefab = prefab.GetComponent<DamageText>();
        }
        public DamageText ReturnDamageText(int damage, Vector3 pos)
        {
            foreach (DamageText damageText in _objectPool)
            {
                if (!damageText.gameObject.activeSelf)
                {
                    damageText.gameObject.SetActive(true);

                    damageText.Initialize(damage.ToString());
                    damageText.transform.position = pos + RandomOffset();

                    return damageText;
                }
            }
            DamageText newProjectileBehaviour = GameObject.Instantiate(_prefab, pos + RandomOffset(), Quaternion.identity);
            _objectPool.Add(newProjectileBehaviour);
            newProjectileBehaviour.Initialize(damage.ToString());
            return newProjectileBehaviour;
        }

        public DamageText ReturnCustomDamageText(string customText, Vector3 pos)
        {
            foreach (DamageText damageText in _objectPool)
            {
                if (!damageText.gameObject.activeSelf)
                {
                    damageText.gameObject.SetActive(true);

                    damageText.Initialize(customText);
                    damageText.transform.position = pos + RandomOffset();

                    return damageText;
                }
            }
            DamageText newProjectileBehaviour = GameObject.Instantiate(_prefab, pos + RandomOffset(), Quaternion.identity);
            _objectPool.Add(newProjectileBehaviour);
            newProjectileBehaviour.Initialize(customText);
            return newProjectileBehaviour;
        }

        private Vector3 RandomOffset() => new Vector3(UnityEngine.Random.Range(-1*_delay, _delay), UnityEngine.Random.Range(-1*_delay, _delay), 0);
    }
}