using Microsoft.Extensions.Logging; //Add to test project.
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using WpfAppDocFxWLoggingZipTestProject1.Properties;
using WPFAppWithDocFxLoggingAndZip2;

namespace WpfAppDocFxWLoggingZipTestProject1
{
    public partial class WpftestPropertiesValues : INotifyPropertyChanged
    {
        public Random FileRandom = new();
        public char Quote = (char)34;
        public StringBuilder CurrentStringBuilder = new();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        XNamespace ns = Settings.Default.urnSchemasMicrosoftComUnattend; //Add Resources Value = "urn:schemas-microsoft-com:unattend"
        public XNamespace Ns
        {
            get => ns;
            set
            {
                if (ns == value)
                {
                    return;
                }

                ns = value;
                OnPropertyChanged(nameof(Ns));
            }
        }
        XNamespace ns1 = Settings.Default.httpSchemasMicrosoftComWMIConfig2002State; //Add Recources Value = "http://schemas.microsoft.com/WMIConfig/2002/State"
        public XNamespace Ns1
        {
            get => ns1;
            set
            {
                if (ns1 == value)
                {
                    return;
                }

                ns1 = value;
                OnPropertyChanged(nameof(Ns1));
            }
        }
    }
}

