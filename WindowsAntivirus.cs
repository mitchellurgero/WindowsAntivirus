using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace VSAWF
{
    /// <summary>
    /// Windows Antivirus Module
    /// </summary>
    class WindowsAntivirus
    {
        /// <summary>
        /// Get list of currently installed antivirus software.
        /// </summary>
        /// <returns></returns>
        public List<string> GetInstalled()
        {
            List<string> av = new List<string>();
            try
            {
                using (var searcher = new ManagementObjectSearcher(@"\\" + Environment.MachineName + @"\root\SecurityCenter2", "SELECT * FROM AntivirusProduct"))
                {
                    var searcherInstance = searcher.Get();
                    foreach (var instance in searcherInstance)
                    {
                        av.Add(instance["displayName"].ToString());
                    }

                }
            }catch(Exception ex)
            {
                av.Clear();
                return av;
            }
            return av;
        }
        /// <summary>
        /// Detect if antivirus is Active on the current machine.
		/// Only works for Windows Defender for now. Will include more later.
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(@"\\" + Environment.MachineName + @"\root\SecurityCenter2", "SELECT * FROM AntivirusProduct"))
                {
                    var searcherInstance = searcher.Get();
                    foreach (var instance in searcherInstance)
                    {
			switch (instance["productState"].ToString())
			{
				case "393472":
					return false;
					break;
				case "397584":
					return true;
					break;
				case "397568":
					return true;
					break;
				default:
					return false;
					break;
			}
                        
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

            return false;
        }
        /// <summary>
        /// Get state of AV. Returns as string, but will always be a number.
        /// </summary>
        /// <returns></returns>
        public string GetState()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(@"\\" + Environment.MachineName + @"\root\SecurityCenter2", "SELECT * FROM AntivirusProduct"))
                {
                    var searcherInstance = searcher.Get();
                    foreach (var instance in searcherInstance)
                    {
                        return instance["productState"].ToString();
                    }

                }
            }
            catch(Exception ex)
            {
                return "000000";
            }


            return "000000";
        }

    }
}
