namespace AI
{
    public class MeleeBot : EnemyBot
    {
        protected override void Attack()
        {
            _animator.SetBool("IsCloseToTarget", _distanceToTarget < StoppingDistance);
        }
    }
}