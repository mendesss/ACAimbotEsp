using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACAimbotEsp
{
    public class Offsets
    {
        public static int
            iViewMatrix = 0x17DFFC-0x6C+ 0x4*16,
            iLocalPlayer = 0x0017E0A8,
            iEntityList = 0x0018AC04,

            vHead = 0x4,
            vFeet = 0x28,
            vAngles = 0x34,
            iHealth = 0xEC,
            iDead = 0xB4,
            sName = 0x205,
            iTeam = 0x30C,
            iCurrentAmmo
            ;
    }
}
