using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http.Json;
using System.Xml;
using Newtonsoft.Json;

namespace server.src.common
{
    public enum Type { 
        Error,
        Success
    }
    public class SQLServerResponse { 
        public Type Type { get; set; }
        public string Result { get; set; } = string.Empty;
        public SQLServerResponse() { }
        public SQLServerResponse(Type t ,string result) {
            Type = t;
            Result = result;
        }
        public override string ToString()
        {
            return "Type: " + Type + " Result:" + Result;
        }
    }
    public class DataBaseClient
    {
        private record Config(string conectionString);
        
        private readonly Config _config;
        
        public DataBaseClient() {

            try
            {
                using (var reader = new StreamReader("config.json"))
                {
                    string json = reader.ReadToEnd();
                    var config = System.Text.Json.JsonSerializer.Deserialize<Config>(json);
                    if (config != null)
                        _config = config;
                    else
                    {
                        throw new Exception("Cannot init database client: faild to initailze configuration");
                    }
                }
                
            }catch (Exception ex)
            {
                throw new Exception("Cannot init database client: "+ex.Message);
            }
        }
        public DataBaseClient(string conectionString)
        {
            if (string.IsNullOrEmpty(conectionString)) {
                throw new Exception("Cannot init database client: Connection string is empty");
            }
            _config = new Config(conectionString);
        }

        public string GetSelectReult(SqlDataReader dataReader) {
            var listName = new List<string>();
            var dataTable = new DataTable("Result");

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                listName.Add(dataReader.GetName(i));
                dataTable.Columns.Add(dataReader.GetName(i),dataReader.GetFieldType(i));
            }
            
            
            while (dataReader.Read())
            {
                var row = dataTable.NewRow();
                foreach (var name in listName) {
                    row[name] = dataReader.GetValue(name);
                }
                dataTable.Rows.Add(row);
            }
            
            return JsonConvert.SerializeObject(dataTable, Newtonsoft.Json.Formatting.Indented); ;
        
        }
        public SQLServerResponse ExecuteQuery(string query) {

            var result = new SQLServerResponse();
            using (var connection = new SqlConnection(_config.conectionString)) {
                connection.Open();
                try
                {
                    using (var sqlCommand = new SqlCommand(query, connection))
                    {
                        using (var dataReader = sqlCommand.ExecuteReader()) { 
                            
                            if (query.ToLower().Contains("select"))
                            {
                                result.Result = GetSelectReult(dataReader);
                            }
                            
                            result.Type = Type.Success;
                            return result;
                        }

                    }
                }catch(Exception ex)
                {
                    return new SQLServerResponse(Type.Error, ex.Message);
                }
            }
        }
    }
}
