namespace View
{
    public interface ITakingDamage 
    {
        public void Damage(float damageCount);

        public bool Kill();
    }
}
