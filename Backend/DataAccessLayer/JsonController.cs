using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace IntroSE.ForumSystem.DataAccessLayer
{
    internal class JsonController
    {
        //protected const string _basePath = "persistance";
        //private static JsonSerializerOptions options = new JsonSerializerOptions
        //{
        //    WriteIndented = true
        //};

        //private string SanitizeFilename(string filename)
        //{
        //    var invalidChars = Path.GetInvalidFileNameChars();
        //    var sanitizedFileName = filename;
        //    foreach (char c in invalidChars)
        //    {
        //        sanitizedFileName = sanitizedFileName.Replace(c.ToString(), "");
        //    }
        //    return sanitizedFileName;
        //}

        //public List<T> LoadAll<T>(System.Type type)
        //{
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), _basePath, type.Name);
        //    string[] filenames = Directory.GetFiles(path);
        //    return filenames
        //        .Where(n=>n.EndsWith(".json"))
        //        .Select(n=>Path.GetFileName(n))
        //        .Select(n => Load<T>(type, n))
        //        .ToList();
        //}


        //public T Load<T>(System.Type type, string filename)
        //{
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), _basePath, type.Name, SanitizeFilename(filename));
        //    string content = File.ReadAllText(path);
        //    return FromJson<T>(content);
        //}
        //for generic save
        //public void Write(string filename, object obj) 
        //{
        //    string subDirectory = obj.GetType().Name;
        //    string content = ToJson(obj);
        //    string sanitizedFileName = SanitizeFilename(filename) + ".json";            
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), _basePath, subDirectory);
        //    string fullPath = Path.Combine(path, sanitizedFileName);
        //    Directory.CreateDirectory(path);
        //    File.WriteAllText(fullPath, content);
        //}

        //private T FromJson<T>(string json)
        //{
        //    return JsonSerializer.Deserialize<T>(json);
        //}

        //private string ToJson(object obj)
        //{
        //    return JsonSerializer.Serialize(obj, obj.GetType(), options);
        //}

        ///change for classשיעור 4 none generic and none false object creation
        //none generic for object creation in datalayer only
        protected const string basePath = "persistance";
        protected const string subDirectory = "users"; //You can always use the obj.GetType().Name; we used last week instead
        internal string Read(string filename)
        {
            return File.ReadAllText(filename);
        }
        internal void Write(string filename, string contents)//save to filesystem
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var cleanFileName = filename;
            foreach (char c in invalidChars)
            {
                cleanFileName = cleanFileName.Replace(c.ToString(), "");
            }
            cleanFileName += ".json";
            string path = Path.Combine(Directory.GetCurrentDirectory(), basePath, subDirectory);
            string fullPath = Path.Combine(path, cleanFileName);
            Directory.CreateDirectory(path);
            File.WriteAllText(fullPath, contents);

        }
        private string[] GetUserNames()//can be used to get list of user in system. Or you can use another file for that.
        {
            if (Directory.Exists(Path.Combine(basePath, subDirectory)))
            {
                return Directory.GetFiles(Path.Combine(basePath, subDirectory));
            }
            return new List<string>().ToArray();
        }

        internal List<User> LoadAllUsers()
        {
            List<User> ans = new List<User>();
            foreach (var email in this.GetUserNames())
            {
                User u = this.ImportUser(email);
                ans.Add(u);
            }
            return ans;
        }

        public User ImportUser(string path)
        {
            return FromJson(Read(path));
        }

        private User FromJson(string json)
        {
            return JsonSerializer.Deserialize<User>(json);//error here!!!!!! 
        }
    }
}
