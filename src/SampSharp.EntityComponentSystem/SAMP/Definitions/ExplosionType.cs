namespace SampSharp.EntityComponentSystem.Definitions
{
    /// <summary>
    /// Contains all types of explosions with description
    /// </summary>
    public enum ExplosionType
    {
        /// <summary>
        /// Size large. Visible. Damage.
        /// </summary>
        LargeVisibleDamage,


        /// <summary>
        /// Size normal. Visible. Creates a fire.
        /// </summary>
        NormalVisibleFire,


        /// <summary>
        /// Size large. Visible. Damage. Creates a fire.
        /// </summary>
        LargeVisibleDamageFire,


        /// <summary>
        /// Size large. Visible. Damage. Sometimes it does not create a fire.
        /// </summary>
        LargeVisibleDamageFire2,


        /// <summary>
        /// Size normal. Visible. Damage. It represents a vanishing flash. No sound.
        /// </summary>
        NormalVisibleDamageFlash,


        /// <summary>
        /// Size normal. Visible. Damage. It represents a vanishing flash. No sound.
        /// </summary>
        NormalVisibleDamageFlash2,


        /// <summary>
        /// Size very large. Visible. Damage. Additional reddish explosion after-glow.
        /// </summary>
        VeryLargeVisibleDamage,


        /// <summary>
        /// Size huge. Visible. Damage. Additional reddish explosion after-glow.
        /// </summary>
        HugeVisibleDamage,


        /// <summary>
        /// Size normal. Invisible. Damage.
        /// </summary>
        NormalInvisibleDamage,


        /// <summary>
        /// Size normal. Damage. Creates a fire at ground level, otherwise explosion is heard but invisible.
        /// </summary>
        NormalInvisibleDamageFire,


        /// <summary>
        /// Size large. Visible. Damage. Compared to the LargeVisibleDamage, the explosion seems great.
        /// </summary>
        LargeVisibleDamage2,


        /// <summary>
        /// Size small. Visible. Damage.
        /// </summary>
        SmallVisibleDamage,


        /// <summary>
        /// Size very small. Visible. Damage.
        /// </summary>
        VerySmallVisibleDamage,


        /// <summary>
        /// Size large. Invisible. Produces no special effects other than black burn effects on the ground.
        /// </summary>
        LargeInvisible
    }
}
