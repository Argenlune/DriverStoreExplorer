﻿using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Rapr
{
    public static class Program
    {
        private static Assembly ResolveEventHandler(Object sender, ResolveEventArgs args)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            String resourceName = string.Format("{0}.{1}.dll", assembly.EntryPoint.DeclaringType.Namespace, new AssemblyName(args.Name).Name);

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveEventHandler;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DSEForm());
        }
    }
}
