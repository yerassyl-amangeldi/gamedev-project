public class Heart : Hazard
{
    public override bool Shootable => false;

    protected override void OnHitPlayer(Health p)
    {
        p.Heal(1);
    }
}
