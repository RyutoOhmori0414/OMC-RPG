namespace RPG.Battle.Skill
{
    public interface ISkill
    {
        public void UseSkill(IDamageable user, IDamageable[] targets);
    }   
}