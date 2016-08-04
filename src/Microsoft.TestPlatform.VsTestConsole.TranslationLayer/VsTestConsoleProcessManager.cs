// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.TestPlatform.VsTestConsole.TranslationLayer
{
    using Interfaces;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Vstest.console.exe process manager
    /// </summary>
    internal class VsTestConsoleProcessManager : IProcessManager
    {
        private string vstestConsolePath;

        private object syncObject = new object();

        private bool vstestConsoleStarted = false;

        private bool vstestConsoleCrashed = false;

        private Process process;

        #region Constructor

        public VsTestConsoleProcessManager(string vstestConsolePath)
        {
            this.vstestConsolePath = vstestConsolePath;
        }

        #endregion Constructor

        public bool IsProcessInitialized()
        {
            lock(syncObject)
            {
                return this.vstestConsoleStarted && !vstestConsoleCrashed && 
                    this.process != null && !this.process.HasExited;
            }
        }

        /// <summary>
        /// Call xUnit.console.exe with the parameters previously specified
        /// </summary>
        public void StartProcess(string[] args)
        {
            using (this.process = new Process())
            {
                process.StartInfo.FileName = vstestConsolePath;
                if (args != null)
                {
                    process.StartInfo.Arguments = args.Length < 2 ? args[0] : string.Join(" ", args);
                }
                //process.StartInfo.WorkingDirectory = WorkingDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                //process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.RedirectStandardError = true;

                EqtTrace.Verbose("VsTestCommandLineWrapper: {0} {1}", process.StartInfo.FileName, process.StartInfo.Arguments);

                process.Exited += Process_Exited;
                process.Start();

                lock (syncObject)
                {
                    vstestConsoleStarted = true;
                }
            }
        }

        public void ShutdownProcess()
        {
            // Ideally process should die by itself
            if(IsProcessInitialized())
            {
                this.process.Kill();
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            lock (syncObject)
            {
                vstestConsoleCrashed = true;
            }
        }
    }
}