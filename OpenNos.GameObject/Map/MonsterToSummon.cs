﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

namespace OpenNos.GameObject
{
    public class MonsterToSummon
    {
        #region Instantiation

        public MonsterToSummon(short vnum, MapCell spawnCell, long target, bool move)
        {
            VNum = vnum;
            SpawnCell = spawnCell;
            Target = target;
            IsMoving = move;
        }

        #endregion

        #region Properties

        public bool IsMoving { get; set; }

        public MapCell SpawnCell { get; set; }

        public long Target { get; set; }

        public short VNum { get; set; }

        #endregion
    }
}