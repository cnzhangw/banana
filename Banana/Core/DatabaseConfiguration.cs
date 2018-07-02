// <copyright company="PetaPoco - CollaboratingPlatypus">
//      Apache License, Version 2.0 https://github.com/CollaboratingPlatypus/PetaPoco/blob/master/LICENSE.txt
// </copyright>
// <author>PetaPoco - CollaboratingPlatypus</author>
// <date>2016/01/10</date>

using Banana.Core.Interface;
using System;
using System.Collections.Generic;

namespace Banana.Core
{
    /// <summary>
    ///     A helper class which enables fluent configuration.
    /// </summary>
    public class DatabaseConfiguration : IDatabaseBuildConfiguration, IBuildConfigurationSettings, IHideObjectMethods
    {
        private readonly IDictionary<string, object> _settings = new Dictionary<string, object>();

        /// <summary>
        ///     Private constructor to force usage of static build method.
        /// </summary>
        private DatabaseConfiguration()
        {
        }

        void IBuildConfigurationSettings.SetSetting(string key, object value)
        {
            // Note: no argument checking because, pref, enduser unlikely and handled by RT/FW
            if (value != null)
                _settings[key] = value;
            else
                _settings.Remove(key);
        }

        void IBuildConfigurationSettings.TryGetSetting<T>(string key, Action<T> setSetting, Action onFail = null)
        {
            // Note: no argument checking because, pref, enduser unlikely and handled by RT/FW
            object setting;
            if (_settings.TryGetValue(key, out setting))
                setSetting((T) setting);
            else if (onFail != null)
                onFail();
        }

        /// <summary>
        ///     Starts a new PetaPoco build configuration.
        /// </summary>
        /// <returns>An instance of <see cref="IDatabaseBuildConfiguration" /> to form a fluent interface.</returns>
        public static IDatabaseBuildConfiguration Build()
        {
            return new DatabaseConfiguration();
        }
    }
}