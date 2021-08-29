namespace Entities
{
    public class BotHealth : Health
    {
        protected override void Die()
        {
            Destroy(this.gameObject);
        }
    }
}