using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Sa_Installer.Models.Registry
{
    class ReadWriteRegistry
    {
        public static bool WriteRegistry(RegistryKey BaseFolder, string SubFolder, string KeyName, object Value, RegistryValueKind ValueKind)
        {
            try
            {
                RegistryKey RegistryKey = BaseFolder.CreateSubKey(SubFolder);
                RegistryKey.SetValue(KeyName, Value, ValueKind);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static (bool, object) ReadRegistry(RegistryKey BaseFolder, string SubFolder, string ValueName)
        {
            try
            {
                RegistryKey key = BaseFolder.OpenSubKey(SubFolder);
                if (key == null)
                {
                    return (false, null);
                }
                else
                {
                    var value = key.GetValue(ValueName);
                    if (value == null)
                    {
                        return (false, null);
                    }
                    else
                    {
                        return (true, value);
                    }
                }
            }
            catch
            {
                return (false, null);
            }
        }

        public static (bool, string[]) KeyNames(RegistryKey BaseFolder, string SubFolder)
        {
            try
            {
                RegistryKey keyName = BaseFolder.OpenSubKey(SubFolder);
                if (keyName != null)
                {
                    return (true, keyName.GetSubKeyNames());
                }
                else
                {
                    return (false, null);
                }
            }
            catch
            {
                return (false, null);
            }
        }
    }
}
