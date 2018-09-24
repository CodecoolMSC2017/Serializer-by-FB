using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    [Serializable]
    class Person : IDeserializationCallback
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [NonSerialized]
        public int serial;


        public Person() { }

        public Person(String name, DateTime birthDate, String email, String phone)
        {
            this.Name = name;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Phone = phone;
        }

        public void Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            for (int i = 0; i < 100; i++)
            {
                string filename = "person" + i + ".dat";
                if (!File.Exists(filename))
                {
                    Stream stream = new FileStream(filename,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, this);
                    stream.Close();
                    break;
                }
            }

        }

        public static Person GetFirstDeserialized()
        {
            IFormatter formatter = new BinaryFormatter();
            string filename = "person0.dat";
            if (File.Exists(filename))
            {
                Stream stream = new FileStream("person0.dat",
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
                Person p = (Person)formatter.Deserialize(stream);
                p.serial = 0;
                stream.Close();
                return p;
            }
            else
            {
                Person p = new Person("Janos Pal Papa", new DateTime(1996, 9, 13), "semmiertelme13@gmail.com", "06702892745");
                p.Serialize();
                p.serial = 0;
                return p;
            }
        }

        public static Person GetLastDeserialized()
        {
            IFormatter formatter = new BinaryFormatter();
            for (int i = 99; i > 0; i--)
            {
                string filename = "person" + i + ".dat";
                if (File.Exists(filename))
                {
                    Stream stream = new FileStream(filename,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                    Person p = (Person)formatter.Deserialize(stream);
                    stream.Close();
                    int resultIndex = filename.IndexOf("person");
                    string splitted = "";
                    int serial = 0;
                    if (resultIndex != -1)
                    {
                        splitted = filename.Substring(resultIndex + 6);
                        serial = Int32.Parse(splitted.Split('.')[0]);
                        Form1.serialNumber = serial;
                        p.serial = serial;
                    }
                    return p;
                }
            }
            throw new FileNotFoundException();
        }

        public static Person GetDeserialized(int serial)
        {
            IFormatter formatter = new BinaryFormatter();
            string filename = "person" + serial + ".dat";
            if (File.Exists(filename))
            {
                Stream stream = new FileStream(filename,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
                Person p = (Person)formatter.Deserialize(stream);
                stream.Close();
                p.serial = serial;
                return p;
            }
            else
            {
                Stream stream = new FileStream("person0.dat",
                      FileMode.Open,
                      FileAccess.Read,
                      FileShare.Read);
                Person p = (Person)formatter.Deserialize(stream);
                stream.Close();
                p.serial = 0;
                return p;
            }
        }

        public void OnDeserialization(object sender)
        {
        }
    }
}
