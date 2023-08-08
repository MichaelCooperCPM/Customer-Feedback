using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // for the File class
using System.Xml; // for XML

namespace Customer_Feedback
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckXML();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // change the label value to the value on the trackbar, when it is scrolled
            helpLabel.Text = trackBar1.Value.ToString();
        }

        void CheckXML()
        {
            if (File.Exists("feedback.xml"))
            {
                SaveDataToXML();
            }
            else
            {
                CreateXML();
            }
        }

        void CreateXML()
        {
            // get the time the user clicks the button
            string now = DateTime.Now.ToString();

            // create settings, set indent to true
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            // create the xml file with applied settings
            XmlWriter writer = XmlWriter.Create("feedback.xml",settings);
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("all_feedback");
                writer.WriteStartElement("feedback");
                writer.WriteStartAttribute("date_time");
                writer.WriteString(now);
                writer.WriteEndAttribute();
                writer.WriteStartElement("q1");
                writer.WriteString(Q1Feedback());
                writer.WriteEndElement();
                writer.WriteStartElement("q2");
                writer.WriteString(Q2Feedback());
                writer.WriteEndElement();
                writer.WriteStartElement("q3");
                writer.WriteString(Q3Feedback());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        void SaveDataToXML()
        {
            // get time user pressed button
            string now = DateTime.Now.ToString();

            // load existing XML file
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("feedback.xml");

            // identify the root elment
            XmlElement root = xDoc.DocumentElement;

            // create new parent with attribute
            XmlElement feedback = xDoc.CreateElement("feedback");
            feedback.SetAttribute("date_time", now);

            // create new q1, add value, append to parent
            XmlElement q1 = xDoc.CreateElement("q1");
            q1.InnerText = Q1Feedback();
            feedback.AppendChild(q1);

            // create new q2, add value, append to parent
            XmlElement q2 = xDoc.CreateElement("q2");
            q2.InnerText = Q2Feedback();
            feedback.AppendChild(q2);

            // create new q3, add value, append to parent
            XmlElement q3 = xDoc.CreateElement("q3");
            q3.InnerText = Q3Feedback();
            feedback.AppendChild(q3);

            //append parent to root, save file
            root.AppendChild(feedback);
            xDoc.Save("feedback.xml");
        }

        string Q1Feedback()
        {
            if (radioButton1.Checked)
            {
                return "lessThan10";
            }
            else if (radioButton2.Checked)
            {
                return "upTo1";
            }
            else if (radioButton3.Checked)
            {
                return "upTo2";
            }
            else if (radioButton4.Checked)
            {
                return "over2";
            }
            else
            {
                return null;
            }
        }

        string Q2Feedback()
        {
            return trackBar1.Value.ToString();
        }

        string Q3Feedback()
        {
            return richTextBox1.Text.ToString();
        }

    }
}
