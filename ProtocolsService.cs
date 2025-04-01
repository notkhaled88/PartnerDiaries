using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace PartnerDiaries
{
    public class ProtocolsService
    {
        public List<ProtocolsClass> Protocols { get; set; }
        public string filePath { get; set; }

        private bool pathExist = false;
        public ProtocolsService(string _path)
        {
            this.filePath = _path;
            readProtocols();
            printAllProtocolls();
        }
        private void printAllProtocolls()
        {
            if (pathExist)
            {

                foreach (ProtocolsClass protocol in this.Protocols)
                {
                    Console.WriteLine(protocol.ProtocolName);
                }
            }
        }

        public ProtocolsClass getProtocoll(string ProtocolName)
        {
            if (pathExist)
            {

                foreach (ProtocolsClass prot in this.Protocols)
                {
                    if (prot.ProtocolName == ProtocolName)
                    {
                        return prot;
                    }
                }
            }
            return null;
        }
        public void writeProtocols()
        {
            if (pathExist)
            {
                string json = JsonSerializer.Serialize(this.Protocols, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(this.filePath, json);
            }
        }
        public void addProtocol(string _ProtocolName, string _ProtocolPoints)
        {
            if (pathExist)
            {
                this.Protocols.Add(new ProtocolsClass() { ProtocolName = _ProtocolName, ProtocolPoints = _ProtocolPoints });
            }
        }

        public void editProtocol(string _ProtocolName, string _ProtocolPoints)
        {
            foreach (ProtocolsClass pro in this.Protocols)
            {
                if (pro.ProtocolName == _ProtocolName)
                {
                    pro.ProtocolPoints = _ProtocolPoints;
                }
            }
        }
        public void deleteProtocol(string _ProtocolName)
        {
            for (int i = 0; i < this.Protocols.Count; i++)
            {
                if (this.Protocols[i].ProtocolName == _ProtocolName)
                {
                    this.Protocols.RemoveAt(i);
                    this.writeProtocols();
                    return;
                }
            }
        }
        private void readProtocols()
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);

                this.Protocols = JsonSerializer.Deserialize<List<ProtocolsClass>>(json);
                this.pathExist = true;

            }
            else
            {
                Console.WriteLine("JSON file not found.");

            }

        }

    }
    public class ProtocolsClass
    {
        public string ProtocolName { get; set; }
        public string ProtocolPoints { get; set; }
    }

}
