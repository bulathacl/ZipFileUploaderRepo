using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ControlPanel.Domain
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json")
        { }
    }
}
