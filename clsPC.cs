using System.Net;
using System.Net.NetworkInformation;

namespace WpfApplication1 {
    class clsPC {
        string _pcname;
        string _ipaddress;
        string _mac = "";

        public string GetPCname { get { return _pcname; } }
        public string GetIPAddress { get { return _ipaddress; } }
        public string GetMAC { get { return _mac; } }

        public int WorkstationInfo() {
            try {

                _pcname = Dns.GetHostName();
                _ipaddress = "0.0.0";

                IPHostEntry host;

                host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ip in host.AddressList) {
                    if (ip.AddressFamily.ToString() == "InterNetwork") _ipaddress = ip.ToString();
                }

                foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces()) {
                    if (n.OperationalStatus == OperationalStatus.Up) { _mac += n.GetPhysicalAddress().ToString(); }
                }

                return 0;
            } catch { return -1; }
        }
    }
}

//Snippet
//clsPC pc = new clsPC();

//pc.WorkstationInfo();
//tslIP.Text = pc.GetIPAddress;
//tslPC.Text = pc.GetPCname;
//txtmacc.text = pc.GetMAC;