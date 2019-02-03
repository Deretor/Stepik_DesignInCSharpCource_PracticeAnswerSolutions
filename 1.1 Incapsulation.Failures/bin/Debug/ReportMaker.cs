using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{

    public class Common
    {
        public static int IsFailureSerious(int failureType)
        {
            if (failureType%2==0) return 1;
            return 0;
        }

        public static int Earlier(object[] v, int day, int month, int year)
        {
            int vYear = (int)v[2];
            int vMonth = (int)v[1];
            int vDay = (int)v[0];
            if (vYear < year) return 1;
            if (vYear > year) return 0;
            if (vMonth < month) return 1;
            if (vMonth > month) return 0;
            if (vDay < day) return 1;
            return 0;
        }

        public static Device DeviceDictToDeviceClass(Dictionary<string, object> deviceDict)
        {
            object deviceName;
            object deviceId;
            int devId;
            deviceDict.TryGetValue("Name", out deviceName);
            deviceDict.TryGetValue("DeviceId", out deviceId);
            if (deviceId == null)
            {
                //return null;
                throw new Exception("Invalid deviceId: deviceId is null");
                //Console.WriteLine("Invalid deviceId: deviceId is null");
                //devId = default(int);
            }
            try
            {
                devId = (int)deviceId;
            }
            catch (Exception e)
            {
                throw new Exception($"Invalid deviceId: {e.Message}, deviceId = {deviceId}");
                //Console.WriteLine($"Invalid deviceId: {e.Message}, deviceId = {deviceId}");
                //devId = default(int);
            }
            return new Device() { Id = devId, Name = deviceName as string };
        }

    }

    public class Device {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Failure {
            public FailureType FailureType { get; set; }
            public DateTime Date { get; set; }
            public int DeviceId { get; set; }
            public bool IsSerious { get {
               return this.FailureType == FailureType.UnexpectedShutdown || this.FailureType == FailureType.HardwareFailures;
            } }
    }

    public enum FailureType {

        UnexpectedShutdown = 0, 
        ShortNonResponding = 1,
        HardwareFailures = 2,
        ConnectionProblems = 3
    }

    public class ReportMaker
    {

       

        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            // refactored code
            // prepare for new function
            // cast day+month+year to DateTime
            DateTime obsoleteDate = new DateTime(year, month, day);
            // cast device dict list to List<Device>
            List<Device> castedDevices = new List<Device>();            
            castedDevices = devices.Select(s => Common.DeviceDictToDeviceClass(s)).ToList();            
            if (failureTypes.Length != times.Length || failureTypes.Length != deviceId.Length || times.Length != deviceId.Length) {
                throw new Exception($"Length of failures list, length of failure dates list and length of failed deviceIds is not equal ({failureTypes.Length}, {times.Length}, {deviceId.Length})");
            }
            if (!times.All(s => s.Length == 3 && s.All(f => f is int))) {
                throw new Exception($"\"times is invalid\" ");
            }
            List<Failure> castedFailures = failureTypes.Select((s, i) => new Failure()
            {
                FailureType = (FailureType)failureTypes[i],
                Date = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
                DeviceId = deviceId[i]

            }).ToList();
            var newRes = ReportMaker.FindDevicesFailedBeforeDate(obsoleteDate, castedFailures, castedDevices);
            return newRes;
        }

        public static List<string> FindDevicesFailedBeforeDate(DateTime obsoleteDate, List<Failure> failures, List<Device> devices) {
            
            List<int> problenaticDevices = failures.FindAll(s => { return s.IsSerious && s.Date < obsoleteDate;}).Select(s => s.DeviceId).ToList();
            List<string> result = devices.FindAll(s => problenaticDevices.Contains(s.Id)).Select(s => s.Name).ToList();
            return result;
        }

    }
}
