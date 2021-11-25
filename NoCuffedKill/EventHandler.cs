using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using Hints;
using UnityEngine;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Player = Exiled.API.Features.Player;

namespace NoCuffedKill
{
    public class EventHandler
    {
        public static void OnPlayerHurt(HurtingEventArgs ev)
        {
            bool Reflect = NoCuffedKill.config.RelfectDamage;
            bool DetainerDamage = NoCuffedKill.config.DetainerDamage;
            Player A = ev.Attacker;
            Player T = ev.Target;

            if(T.IsCuffed && (A != T.Cuffer || DetainerDamage == false))
            {
                if (A.Side == Exiled.API.Enums.Side.Scp)
                    return; // ends function if attacker is an scp, allowing for damage to be registered.
                if (A.Side != T.Side)
                    ev.IsAllowed = false; // does not allow the damage to be registered if they are on the opposite team and cuffed
                if (Reflect)
                    A.Hurt(ev.Amount);
            }
            else // the else section might not be necessary at all, but i added it just in case.
            {
                if (A.Side != T.Side)
                    ev.IsAllowed = true; // registers damage,
            }
        }
    }
}
