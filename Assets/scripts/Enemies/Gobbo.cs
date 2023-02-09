public class Gobbo : AbstractEnemy
{

    public void Awake()
    {
       recieveDamage = new(RecieveDamage);
    }

    protected void RecieveDamage(int damage)
    {
        hitpoints -= BlockDamage(blockAmmount, damage);

    }
    protected override int BlockDamage(int blockAmmount, int damage)
    {
        return damage - blockAmmount > 0 ? damage - blockAmmount : 1;
    }

    public override void Heal(int points)
    {

    }
}
