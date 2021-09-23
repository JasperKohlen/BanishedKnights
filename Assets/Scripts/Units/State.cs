﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum State
    {
        IDLE,
        REMOVING,
        DESTROYING,
        DELIVERING_TO_BUILD,
        BUILDING,
        DELIVERING_TO_STORAGE,
        WORKING,
        COLLECTING_FROM_STORAGE,
        DELIVERING_TO_BARRACKS
    }
}
