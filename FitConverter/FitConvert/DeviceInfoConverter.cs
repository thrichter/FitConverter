using Dynastream.Fit;
using FitConverter.Sigma;

namespace FitConverter.FitConvert
{
    public class DeviceInfoConverter : IConverter<SmfEntry>
    {
        private readonly IDateTimeService _dateTimeService;

        public DeviceInfoConverter(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public void ProcessSection(SmfEntry source, IFitEncoderAdapter encoder)
        {
            DeviceInfoMesg deviceInfoMesg = new DeviceInfoMesg();

            deviceInfoMesg.SetTimestamp(new DateTime(_dateTimeService.Now));

            if (!string.IsNullOrEmpty(source.Computer.Serial))
            {
                long serial = long.Parse(source.Computer.Serial);
                if (serial < uint.MaxValue)
                {
                    deviceInfoMesg.SetSerialNumber((uint)serial);
                }
            }


            deviceInfoMesg.SetManufacturer(Manufacturer.Sigmasport);
            deviceInfoMesg.SetProduct(901);
            deviceInfoMesg.SetProductName("ROX 9.0 A");
            deviceInfoMesg.SetBatteryStatus(Dynastream.Fit.BatteryStatus.Good);

            encoder.Write(deviceInfoMesg);
        }
    }
}
