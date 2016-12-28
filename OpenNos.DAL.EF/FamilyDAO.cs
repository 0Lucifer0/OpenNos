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

using OpenNos.Core;
using OpenNos.DAL.EF.DB;
using OpenNos.DAL.EF.Helpers;
using OpenNos.Data;
using OpenNos.Data.Enums;
using System;
using System.Linq;

namespace OpenNos.DAL.EF
{
    public class FamilyDAO : MappingBaseDAO<Family, FamilyDTO>, IFamilyDAO
    {
        public DeleteResult Delete(long familyId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    Account Account = context.Account.FirstOrDefault(c => c.AccountId.Equals(familyId));

                    if (Account != null)
                    {
                        context.Account.Remove(Account);
                        context.SaveChanges();
                    }

                    return DeleteResult.Deleted;
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format(Language.Instance.GetMessageFromKey("DELETE_ACCOUNT_ERROR"), familyId, e.Message), e);
                return DeleteResult.Error;
            }
        }

        public SaveResult InsertOrUpdate(ref FamilyDTO family)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    long AccountId = family.FamilyId;
                    Family entity = context.Family.FirstOrDefault(c => c.FamilyId.Equals(AccountId));

                    if (entity == null)
                    {
                        family = Insert(family, context);
                        return SaveResult.Inserted;
                    }
                    else
                    {
                        family = Update(entity, family, context);
                        return SaveResult.Updated;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(string.Format(Language.Instance.GetMessageFromKey("UPDATE_FAMILY_ERROR"), family.FamilyId, e.Message), e);
                return SaveResult.Error;
            }
        }

        public FamilyDTO LoadByCharacterId(long characterId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    FamilyCharacter familyCharacter = context.FamilyCharacter.FirstOrDefault(fc => fc.Character.FirstOrDefault(c=>c.CharacterId.Equals(characterId)) != null);
                    if (familyCharacter != null)
                    {
                        Family family = context.Family.FirstOrDefault(a => a.FamilyId.Equals(familyCharacter.FamilyId));
                        if (family != null)
                        {
                            return _mapper.Map<FamilyDTO>(family);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public FamilyDTO LoadById(long familyId)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    Family family = context.Family.FirstOrDefault(a => a.FamilyId.Equals(familyId));
                    if (family != null)
                    {
                        return _mapper.Map<FamilyDTO>(family);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public FamilyDTO LoadByName(string name)
        {
            try
            {
                using (var context = DataAccessHelper.CreateContext())
                {
                    Family family = context.Family.FirstOrDefault(a => a.Name.Equals(name));
                    if (family != null)
                    {
                        return _mapper.Map<FamilyDTO>(family);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        private FamilyDTO Insert(FamilyDTO family, OpenNosContext context)
        {
            Family entity = _mapper.Map<Family>(family);
            context.Family.Add(entity);
            context.SaveChanges();
            return _mapper.Map<FamilyDTO>(entity);
        }

        private FamilyDTO Update(Family entity, FamilyDTO family, OpenNosContext context)
        {
            if (entity != null)
            {
                entity.Size = family.Size;
                entity.Name = family.Name;
                entity.MaxSize = family.MaxSize;
                entity.FamilyExperience = family.FamilyExperience;
                entity.FamilyLevel = family.FamilyLevel;
                entity.FamilyMessage = family.FamilyMessage;

                context.SaveChanges();
            }
            return _mapper.Map<FamilyDTO>(entity);
        }
    }
}