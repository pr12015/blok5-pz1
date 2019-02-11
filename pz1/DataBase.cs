using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using pz1.Models;

namespace pz1
{
    public class DataBase
    {
        private string _path;

        public string Path { get; }

        public DataBase(string path)
        {
            _path = path;
        }

        public void Write(User user)
        {
            // if (Read(user.UserName) != null)
            // throw new Exception("Username already exists.");

            var serializer = new XmlSerializer(typeof(User));
            using (var writer = new StreamWriter(_path, true))
            {
                serializer.Serialize(writer, user);
            }
        }

        // TODO: Make it generic.
        public void Write(List<User> user)
        {
            // if (Read(user.UserName) != null)
            // throw new Exception("Username already exists.");

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUsers"));
            using (var writer = new StreamWriter(_path, true))
            {
                serializer.Serialize(writer, user, ns);
            }
        }

        public void WriteModified(List<User> user)
        {
            // if (Read(user.UserName) != null)
            // throw new Exception("Username already exists.");

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUsers"));
            using (var writer = new StreamWriter(_path, true))
            {
                serializer.Serialize(writer, user, ns);
            }
        }


        // TODO: Fix read method
        public User Read()
        {
            var serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUsers"));
            try
            {
                using (var reader = new StreamReader(_path))
                {
                    var users = serializer.Deserialize(reader);
                    return users as User;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
