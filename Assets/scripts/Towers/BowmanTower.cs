using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowmanTower : AbstractTower
{
    private void Awake()
    {
        availableTargets = new List<GameObject>();
        currentAttack = new(StandartAttack);
        circleCollider = GetComponent<CircleCollider2D>();
        attackRange = circleCollider.radius;
    }
    private void Update()
    {
        currentTimeTillAttack -= Time.deltaTime;
        if (currentTimeTillAttack < 0)
        {
            currentTimeTillAttack = attackDelay;
            currentAttack(FindTarget(), attackDamage);
        }
    }
    protected  void StandartAttack(AbstractEnemy abstractEnemy, int damage)
    {
        if(abstractEnemy!=null) abstractEnemy.recieveDamage(damage);
    }

    public override AbstractEnemy FindTarget()
    {
        float currentDistance = Mathf.Infinity;
        GameObject closestGameObject = null;
        if (availableTargets.Count > 0)
        {
            foreach (GameObject AvgameObject in availableTargets)
            {
                if ((transform.position - AvgameObject.transform.position).magnitude < currentDistance)
                {
                    closestGameObject = AvgameObject;
                    currentDistance = (transform.position - AvgameObject.transform.position).magnitude;
                }
                if ((transform.position - AvgameObject.transform.position).magnitude < attackRange) Debug.LogWarning("Awailable outranged targed error");
            }
        }
        else Debug.Log("null targets");
        Debug.Log(closestGameObject);
        return closestGameObject.GetComponent<AbstractEnemy>();
    }

    public override void RecieveExperience(int exp)
    {
        Experience += exp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag(targetTag)) availableTargets.Add(collision.gameObject);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag)) availableTargets.Remove(collision.gameObject);
    }

   
}
