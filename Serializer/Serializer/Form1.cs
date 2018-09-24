using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Serializer
{
    public partial class Form1 : Form
    {
        public static int serialNumber = 0;
        public Form1()
        {
            InitializeComponent();
            Person p = Person.GetFirstDeserialized();
            SetUI(p);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
        string name = NameTxt.Text;
        string email = EmailTxt.Text;
        string phone = PhoneTxt.Text;
        DateTime birthDate = DateTime.Parse(DateTxt.Text);
        Person p = new Person(name, birthDate, email, phone);
        p.Serialize();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            Person p = Person.GetDeserialized(++serialNumber);
            serialNumber = p.serial;
            SetUI(p);
            
        }

        private void SetUI(Person p)
        {
            NameTxt.Text = p.Name;
            EmailTxt.Text = p.Email;
            PhoneTxt.Text = p.Phone;
            DateTxt.Text = p.BirthDate.ToString();
            SerialTxt.Text = "Serial:" + serialNumber;
        }

        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            Person p = Person.GetDeserialized(--serialNumber);
            serialNumber = p.serial;
            SetUI(p);
            
        }

        private void FirstBtn_Click(object sender, EventArgs e)
        {
            serialNumber = 0;
            Person p = Person.GetDeserialized(serialNumber);
            serialNumber = p.serial;
            SetUI(p);
            
        }

        private void LastBtn_Click(object sender, EventArgs e)
        {
            Person p = Person.GetLastDeserialized();
            serialNumber = p.serial;
            SetUI(p);
            
        }
    }
}
