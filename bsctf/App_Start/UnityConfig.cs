﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using bsctf.Controllers;
using bsctf.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using SearchEngine;

namespace bsctf
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\CustomSearch.dll");
            
            ISearch searcher = null;
            if (File.Exists(path))
            {
                var assembly = Assembly.LoadFile(path);

                var type = assembly.ExportedTypes.Single(t => typeof(ISearch).IsAssignableFrom(t));

                searcher = (ISearch)Activator.CreateInstance(assembly.FullName, type.FullName).Unwrap();
            }
            if (searcher == null)
                searcher = new DefaultSearch();

            container.RegisterInstance(typeof(ISearch), searcher);
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
        }
    }
}
