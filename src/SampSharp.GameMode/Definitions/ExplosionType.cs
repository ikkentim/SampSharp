using System;
using System.Collections.Generic;
using System.Text;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    /// Contains all types of explosions with description
    /// </summary>
    public enum ExplosionType
    {
        /// <summary>
        /// Size: large. Visible, physical blast.
        /// </summary>
        Zero,
        /// <summary>
        /// Size: normal. Visible, creates fire.
        /// </summary>
        One,
        /// <summary>
        /// Size: large. Visible, creates fire, physical blast.
        /// </summary>
        Two,
        /// <summary>
        /// Size: large. Visible, sometimes creates fire, physical blast.
        /// </summary>
        Three,
        /// <summary>
        /// Size: normal. Visible, splits, physical blast.
        /// Unusual explosion, produces just special blast burn FX effects and blasts things away, NO SOUND EFFECTS.
        /// </summary>
        Four,
        /// <summary>
        /// Size: normal. Visible, splits, physical blast.
        /// Unusual explosion, produces just special blast burn FX effects and blasts things away, NO SOUND EFFECTS.
        /// </summary>
        Five,
        /// <summary>
        /// Size: very large. Visible, physical blast.
        /// Additional reddish explosion after-glow.
        /// </summary>
        Six,
        /// <summary>
        /// Size: huge. Visible, physical blast.
        /// Additional reddish explosion after-glow.
        /// </summary>
        Seven,
        /// <summary>
        /// Size: normal. Invisible, psysical blast.
        /// </summary>
        Eight,
        /// <summary>
        /// Size: normal. Invisible, creates fire, physical blast.
        /// Creates fires at ground level, otherwise explosion is heard but invisible.
        /// </summary>
        Nine,
        /// <summary>
        /// Size: large. Visible, physical blast.
        /// </summary>
        Ten,
        /// <summary>
        /// Size: small. Visible, physical blast.
        /// </summary>
        Eleven,
        /// <summary>
        /// Size: very small. Visible, physical blast.
        /// </summary>
        Twelve,
        /// <summary>
        /// Size: large. Invisible.
        /// Produces no special effects other than black burn effects on the ground, does no damage either.
        /// </summary>
        Thirteen
    }
}
