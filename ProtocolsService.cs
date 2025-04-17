using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerDiaries
{
    public class Protocol : JsonElement
    {
        public string content { get; set; }
        public Protocol() { }
        public Protocol(Protocol P)
        {
            this.SearchParameter = P.SearchParameter;
            this.content = P.content;
        }
        public Protocol(string _SearchParameter,string _content)
        {
            this.SearchParameter = _SearchParameter;
            this.content = _content;
        }
        public void copy(Protocol j)
        {
            this.SearchParameter = j.SearchParameter;
            this.content = j.content;
        }
    }
    public class ProtocolList : JsonElementsList<Protocol>
    {
        public ProtocolList() { }
    }

    public class ProtocolService:JsonService<ProtocolList, Protocol>
    {
        public ProtocolService(string _path) : base(_path) { }
       
    }
   /*
    public class ProtocolsService
    {
        public _JsonElementslist Protocols;
        private string path { get; set; }
        public ProtocolsService(string _path)
        {
            this.path = _path;
            Protocols = new _JsonElementslist();
            readProtocols();

        }

        private void readProtocols()
        {
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                if(json.Count() > 1)
                {
                    Protocols.jsonElements = JsonSerializer.Deserialize<List<JsonElement>>(json);
                }
                else
                {
                    Protocols.init();
                }
                

            }
            else
            {
                Console.WriteLine("JSON file not found. File is going to be created.");
                using (System.IO.File.Create(path)) { } ;
                readProtocols();
                

            }

        }

        public void writeProtocols()
        {
            string json = JsonSerializer.Serialize(this.Protocols.jsonElements, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(this.path, json);

        }

        public JsonElement getProtocoll(string ProtocolName)
        {
            return Protocols.getElementbyName(ProtocolName);
        }

        public void addProtocol(string _ProtocolName, string _ProtocolPoints)
        {
            Protocols.addElement(new JsonElement() { name = _ProtocolName, content = _ProtocolPoints });
        }
        public void addProtocol(JsonElement j)
        {
            Protocols.addElement(j);
        }

        public void editProtocol(string _ProtocolName, string _ProtocolPoints, string _ProtocolNameOriginal)
        {
            Protocols.editElement(_ProtocolNameOriginal, new JsonElement() { name = _ProtocolName, content = _ProtocolPoints });
        }

        public void deleteProtocol(string _ProtocolName)
        {
            ProtocolsService tempServ = new ProtocolsService(this.path.Replace(".json", "deleted.json"));
            tempServ.addProtocol(this.getProtocoll(_ProtocolName));
            tempServ.writeProtocols();
            this.Protocols.deleteElement(_ProtocolName);
            this.writeProtocols();
        }


        }
    }
 */


   /*
    public class ProtocolsService
    {
        public List<Protocol> Protocols { get; set; }
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

                foreach(Protocol protocol in this.Protocols)
                {
                    Console.WriteLine(protocol.ProtocolName);
                }
            }
        }

        public Protocol getProtocoll(string ProtocolName)
        {
            if (pathExist)
            {

                foreach(Protocol prot in this.Protocols)
                {
                    if(prot.ProtocolName == ProtocolName)
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
                this.Protocols.Add(new Protocol() { ProtocolName = _ProtocolName , ProtocolPoints = _ProtocolPoints });
            }
        }

        public void editProtocol(string _ProtocolName, string _ProtocolPoints, string _ProtocolNameOriginal)
        {
            foreach(Protocol pro in this.Protocols)
            {
                if(pro.ProtocolName == _ProtocolNameOriginal)
                {
                    if(_ProtocolNameOriginal != _ProtocolName)
                    {
                        pro.ProtocolName = _ProtocolName;
                    }
                    pro.ProtocolPoints = _ProtocolPoints;
                    return;
                }
            }
        }
        public void deleteProtocol(string _ProtocolName)
        {
            if (!File.Exists(this.filePath.Replace(".json", "deleted.json")))
            {
                File.Create(this.filePath.Replace(".json", "deleted.json"));
            }
            ProtocolsService tempSer = new ProtocolsService(this.filePath.Replace(".json", "deleted.json"));
            tempSer.readProtocols();
            

            for (int i = 0; i < this.Protocols.Count; i++)
            {
                if (this.Protocols[i].ProtocolName == _ProtocolName)
                {
                    tempSer.addProtocol(this.Protocols[i].ProtocolName, this.Protocols[i].ProtocolPoints);
                    tempSer.writeProtocols();
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
                if (json != "")
                {
                    this.Protocols = JsonSerializer.Deserialize<List<Protocol>>(json);
                }
                else
                {
                    this.Protocols = new List<Protocol>();
                }
                this.pathExist = true;

            }
            else
            {
                Console.WriteLine("JSON file not found.");

            }

        }

    }


    public class Protocol
    {
        public string ProtocolName { get; set; }
        public string ProtocolPoints { get; set; }
    }
    */
}
