namespace RPG.Battle.Player
{
    public interface ISkill
    {
        public void UseSkill(IDamageable user, IDamageable[] targets);
    }   
}