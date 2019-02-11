using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace pz1.Models
{
    [Serializable]
    public class Image
    {
        [XmlElement("Path")]
        private string _path;

        [XmlElement("Title")]
        private string _title;

        [XmlElement("Description")]
        private string _description;

        public Image() { }

        public Image(string path, string title, string description)
        {
            _path = path;
            _title = title;
            _description = description;
        }

        public string Path
        {
            get => _path;

            set
            {
                if (_path == value) return;

                _path = value;
            }
        }

        public string Title
        {
            get => _title;

            set
            {
                if (_title == value) return;

                _title = value;
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                if (_description == value) return;

                _description = value;
            }
        }
    }
}
