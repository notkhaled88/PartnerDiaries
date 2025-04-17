using System.Text.Json;
using System.Xml.Linq;
using System.Text.Json;

namespace PartnerDiaries
{
    public interface IJsonElement
    {
        public string SearchParameter { get; set; }
        public void copy(IJsonElement j);


    }
    public interface IJsonElementsList<IJE> where IJE : IJsonElement, new()
    {
        public List<IJE> elements { get; set; }
        public int getElementIndex(string _SearchParameter);

        public void init();
        public void deleteElement(string _SearchParameter);
        public void editElement(string _SearchParameter, IJE Element);
        public void addElement(IJE j);
        public IJE getElementByName(string _SearchParamter);
    }
    public interface IJsonService<IJEL, IJE>
        where IJEL : IJsonElementsList<IJE>, new()
        where IJE : IJsonElement, new()
    {
        public IJEL jsonElements { get; set; }
        public string path { get; set; }

        public void readJsonElement();

        public void writeJsonElement();
        public IJE getJsonElement(string _SearchParameter);

        public void addJsonElement(IJE J);
        public void editJsonElement(string _SearchParameter, IJE J);
        public void deleteJsonElement(string _SearchParameter);
        public void init(string _path);
        public int getElementIndex(string _SearchParameter);
    }
    public class JsonElement: IJsonElement
    {
        public string SearchParameter { get; set; }
        public virtual void copy(IJsonElement j)
        {
            this.SearchParameter = j.SearchParameter;
        }
    }

    public class JsonElementsList<JE> : IJsonElementsList<JE> where JE : IJsonElement, new()
    {
        public List<JE> elements { get; set; }

        public int getElementIndex(string _SearchParameter)
        {
            for (int i = 0; i < this.elements.Count; i++)
            {
                if (this.elements[i].SearchParameter == _SearchParameter)
                {
                    return i;
                }
            }
            return -1;
        }

        public void init()
        {
            this.elements = new List<JE>();
        }
        public void deleteElement(string _SearchParameter)
        {
            int ind = this.getElementIndex(_SearchParameter);
            this.elements.RemoveAt(ind);
        }
        public void editElement(string _SearchParameter, JE Element)
        {
            int ind = this.getElementIndex(_SearchParameter);
            if (ind != -1)
            {
                this.elements[ind] = Element;
            }
        }
        public void addElement(JE j)
        {
            elements.Add(j);
        }
        public JE getElementByName(string _SearchParameter)
        {
            int ind = this.getElementIndex(_SearchParameter);
            if (ind != -1)
            {
                return this.elements[ind];
            }
            return default(JE);
        }
    }

    public class JsonService<JEL, JE>: IJsonService<JEL , JE>
        where JE: IJsonElement, new()
        where JEL:IJsonElementsList<JE>, new()
    {
        public JsonService(string _path)
        {
            this.path = _path;
            this.jsonElements = new JEL();
            readJsonElement();
        }
        public JEL jsonElements { get; set; }
        public string path { get; set; }
        public void readJsonElement()
        {
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                if (json.Count() > 10)
                {
                    this.jsonElements.elements = JsonSerializer.Deserialize<List<JE>>(json);
                }
                else
                {
                    this.jsonElements.init();
                }
            }
            else
            {
                    File.Create(path).Close();
                    this.jsonElements.init();
            }
        }

        public void writeJsonElement()
        {
            string json = JsonSerializer.Serialize(this.jsonElements.elements, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(this.path, json);
        }
        public JE getJsonElement(string _SearchParameter)
        {
            return this.jsonElements.getElementByName(_SearchParameter);
        }

        public void addJsonElement(JE J)
        {
            this.jsonElements.addElement(J);
        }
        public void editJsonElement(string _SearchParameter, JE J)
        {
            this.jsonElements.editElement(_SearchParameter, J);
        }
        public void deleteJsonElement(string _SearchParameter)
        {
            JsonService<JEL,JE> tempServ = new JsonService<JEL,JE>(this.path.Replace(".json", "deleted.json"));
            tempServ.addJsonElement(this.getJsonElement(_SearchParameter));
            tempServ.writeJsonElement();
            this.jsonElements.deleteElement(_SearchParameter);
            this.writeJsonElement();
        }
        public virtual void init(string _path)
        {
            this.jsonElements.init();
        }
        public int getElementIndex(string _SearchParameter)
        {
            return this.jsonElements.getElementIndex(_SearchParameter);
        }
    }
}
