/*
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

using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using log4net;
using OpenNos.Core;
using OpenNos.Data;
using OpenNos.DAL;
using OpenNos.DAL.EF.Helpers;
using OpenNos.GameObject;
using OpenNos.Handler;
using OpenNos.ServiceRef.Internal;

namespace OpenNos.Login
{
    public class Program
    {
        #region Methods

        public static void Main()
        {
            checked
            {
                try
                {
                    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

                    // initialize Logger
                    Logger.InitializeLogger(LogManager.GetLogger(typeof(Program)));
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                    Console.Title = $"OpenNos Login Server v{fileVersionInfo.ProductVersion}";
                    int port = Convert.ToInt32(ConfigurationManager.AppSettings["LoginPort"]);
                    string text = $"LOGIN SERVER VERSION {fileVersionInfo.ProductVersion} - PORT : {port} by OpenNos Team";
                    int offset = (Console.WindowWidth - text.Length) / 2;
                    Console.WriteLine(new String('=', Console.WindowWidth));
                    Console.SetCursorPosition(offset < 0 ? 0 : offset, Console.CursorTop);
                    Console.WriteLine(text + "\n" +
                    new String('=', Console.WindowWidth) + "\n");

                    // initialize DB
                    if (!DataAccessHelper.Initialize())
                    {
                        Console.ReadLine();
                        return;
                    }

                    Logger.Log.Info(Language.Instance.GetMessageFromKey("CONFIG_LOADED"));

                    try
                    {
                        // register EF -> GO and GO -> EF mappings
                        RegisterMappings();

                        ServiceFactory.Instance.Initialize();
                        NetworkManager<LoginEncryption> networkManager = new NetworkManager<LoginEncryption>("127.0.0.1", port, typeof(LoginPacketHandler), typeof(LoginEncryption), false);

                        // refresh WCF
                        ServiceFactory.Instance.CommunicationService.CleanupAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error("General Error Server", ex);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("General Error", ex);
                    Console.ReadKey();
                }
            }
        }

        private static void RegisterMappings()
        {
            // entities
            DAOFactory.AccountDAO.RegisterMapping(typeof(Account)).InitializeMapper();
            DAOFactory.PenaltyLogDAO.RegisterMapping(typeof(PenaltyLogDTO)).InitializeMapper();
        }

        #endregion
    }
}