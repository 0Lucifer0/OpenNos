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

using AutoMapper;
using OpenNos.DAL;
using OpenNos.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenNos.GameObject
{
    public class Map : MapDTO
    {
        #region Members

        private char[,] _grid;
        private List<MapMonster> _monsters;
        private List<MapNpc> _npcs;
        private List<Portal> _portals;
        private Guid _uniqueIdentifier;
        private int _xLength;
        private int _yLength;

        #endregion

        #region Instantiation

        public Map(short mapId, Guid uniqueIdentifier, byte[] data)
        {
            Mapper.CreateMap<MapDTO, Map>();
            Mapper.CreateMap<Map, MapDTO>();

            MapId = mapId;
            _uniqueIdentifier = uniqueIdentifier;
            Data = data;
            LoadZone();
            IEnumerable<PortalDTO> portalsDTO = DAOFactory.PortalDAO.LoadByMap(MapId);
            _portals = new List<Portal>();
            DroppedList = new Dictionary<long, MapItem>();

            ShopUserList = new Dictionary<long, MapShop>();
            foreach (PortalDTO portal in portalsDTO)
            {
                _portals.Add(new GameObject.Portal()
                {
                    DestinationMapId = portal.DestinationMapId,
                    SourceMapId = portal.SourceMapId,
                    SourceX = portal.SourceX,
                    SourceY = portal.SourceY,
                    DestinationX = portal.DestinationX,
                    DestinationY = portal.DestinationY,
                    Type = portal.Type,
                    PortalId = portal.PortalId,
                    IsDisabled = portal.IsDisabled
                });
            }

            _monsters = new List<MapMonster>();
            foreach (MapMonsterDTO monster in DAOFactory.MapMonsterDAO.LoadFromMap(MapId))
            {
                _monsters.Add(new MapMonster()
                {
                    MapId = monster.MapId,
                    MapX = monster.MapX,
                    MapMonsterId = monster.MapMonsterId,
                    MapY = monster.MapY,
                    MonsterVNum = monster.MonsterVNum,
                    Position = monster.Position,
                    firstX = monster.MapX,
                    firstY = monster.MapY,
                    Move = monster.Move
                });
            }
            IEnumerable<MapNpcDTO> npcsDTO = DAOFactory.MapNpcDAO.LoadFromMap(MapId);

            _npcs = new List<MapNpc>();
            foreach (MapNpcDTO npc in npcsDTO)
            {
                _npcs.Add(new GameObject.MapNpc(npc.MapNpcId)
                {
                    MapId = npc.MapId,
                    MapX = npc.MapX,
                    MapY = npc.MapY,
                    Position = npc.Position,
                    NpcVNum = npc.NpcVNum,
                    IsSitting = npc.IsSitting,
                    Move = npc.Move,
                    Effect = npc.Effect,
                    EffectDelay = npc.EffectDelay,
                    Dialog = npc.Dialog,
                    firstX = npc.MapX,
                    firstY = npc.MapY
                });
            }
        }

        #endregion

        #region Properties

        public IDictionary<long, MapItem> DroppedList { get; set; }

        public int IsDancing { get; set; }

        public List<MapMonster> Monsters
        {
            get
            {
                return _monsters;
            }
        }

        public EventHandler NotifyClients { get; set; }

        public List<MapNpc> Npcs
        {
            get
            {
                return _npcs;
            }
        }

        public List<Portal> Portals
        {
            get
            {
                return _portals;
            }
        }

        public Dictionary<long, MapShop> ShopUserList { get; set; }

        #endregion

        #region Methods

        public bool IsBlockedZone(int x, int y)
        {
            if (x < 1 || y < 1 || x > char.MaxValue || y > char.MaxValue || y > _grid.GetLength(0) || x > _grid.GetLength(1) || _grid[y - 1, x - 1] == 1)
            {
                return true;
            }

            return false;
        }

        public bool IsBlockedZone(int firstX, int firstY, int MapX, int MapY)
        {
            bool ok = false;
            if (MapX > firstX)
            {
                for (int i = 0; i <= MapX - firstX; i++)
                {
                    if (IsBlockedZone(firstX + i, firstY))
                    {
                        ok = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= firstX - MapX; i++)
                {
                    if (IsBlockedZone(MapX + i, MapY))
                    {
                        ok = true;
                    }
                }
            }

            if (MapY > firstY)
            {
                for (int i = 0; i <= MapY - firstY; i++)
                {
                    if (IsBlockedZone(firstX, firstY + i))
                    {
                        ok = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= firstY - MapY; i++)
                {
                    if (IsBlockedZone(MapX, MapY + i))
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }
        public void LoadZone()
        {
            Stream stream = new MemoryStream(Data);

            byte[] bytes = new byte[stream.Length];
            int numBytesToRead = 1;
            int numBytesRead = 0;

            byte[] xlength = new byte[2];
            byte[] ylength = new byte[2];
            stream.Read(bytes, numBytesRead, numBytesToRead);
            xlength[0] = bytes[0];
            stream.Read(bytes, numBytesRead, numBytesToRead);
            xlength[1] = bytes[0];
            stream.Read(bytes, numBytesRead, numBytesToRead);
            ylength[0] = bytes[0];
            stream.Read(bytes, numBytesRead, numBytesToRead);
            ylength[1] = bytes[0];

            _yLength = BitConverter.ToInt16(ylength, 0);
            _xLength = BitConverter.ToInt16(xlength, 0);

            _grid = new char[_yLength, _xLength];
            for (int i = 0; i < _yLength; ++i)
            {
                for (int t = 0; t < _xLength; ++t)
                {
                    stream.Read(bytes, numBytesRead, numBytesToRead);
                    _grid[i, t] = Convert.ToChar(bytes[0]);
                }
            }
        }

        public async void MonsterLifeManager()
        {
            var rnd = new Random();
            Task MonsterLifeTask = null;
            foreach (MapMonster monster in Monsters.OrderBy(i => rnd.Next()))
            {
                MonsterLifeTask = new Task(() => monster.MonsterLife());
                MonsterLifeTask.Start();
                await Task.Delay(rnd.Next(1000 / Monsters.Count(), 1000 / Monsters.Count()));
            }
        }

        public async void NpcLifeManager()
        {
            var rnd = new Random();
            Task NpcLifeTask = null;
            foreach (MapNpc npc in Npcs.OrderBy(i => rnd.Next()))
            {
                NpcLifeTask = new Task(() => npc.NpcLife());
                NpcLifeTask.Start();

                await Task.Delay(rnd.Next(1000 / Npcs.Count(), 1000 / Npcs.Count()));
            }
        }

        internal async void MapTaskManager()
        {
            Task NpcMoveTask = new Task(() => NpcLifeManager());
            NpcMoveTask.Start();
            Task MonsterMoveTask = new Task(() => MonsterLifeManager());
            MonsterMoveTask.Start();
            await NpcMoveTask;
            await MonsterMoveTask;
        }

        #endregion
    }
}