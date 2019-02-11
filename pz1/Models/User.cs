using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace pz1.Models
{
    [Serializable]
    [XmlType("User")]
    public class User
    {
        [XmlElement("UserName")]
        private string _username;

        [XmlElement("Password")]
        private string _password;

        [XmlElement("Image")]
        public List<Image> _images;

        public User() { }

        public User(string username, string password)
        {
            _username = username;
            _password = password;
            _images = new List<Image>();
        }

        public string UserName
        {
            get => _username;

            set
            {
                if (_username == value) return;

                _username = value;
            }
        }

        public string Password
        {
            get => _password;

            set
            {
                if (_password == value) return;

                _password = value;
            }
        }

        public void AddImage(string path, string title, string description)
        {
            foreach (var img in _images)
                if (img.Path == path)
                    return;

            _images.Add(new Image(path, title, description));
        }
    }
}
