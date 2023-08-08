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
            // call the method when Save button clicked
            CheckXML();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // change the label value to the value on the trackbar, when it is scrolled
            helpLabel.Text = trackBar1.Value.ToString();
        }

        // if XML file exists already, just save the entered data, otherwise create a new XML file
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

        //create new XML file and save first set of data (only runs once)
        void CreateXML()
        {
            // get the time the user clicks the button to store as an attribute on the feedback element
            string now = DateTime.Now.ToString();

            // create settings, set indent to true
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            // create the XMLWriter machine called writer to create the xml file called feedback.xml, with settings applied
            XmlWriter writer = XmlWriter.Create("feedback.xml", settings);
            {
                writer.WriteStartDocument();                // write the xml declaration (1.0)
                writer.WriteStartElement("all_feedback");   // open root
                writer.WriteStartElement("feedback");       // open parent
                writer.WriteStartAttribute("date_time");    // open parent attribute
                writer.WriteString(now);                    // insert attribute value
                writer.WriteEndAttribute();                 // end attribute
                writer.WriteStartElement("q1");             // open child
                writer.WriteString(Q1Feedback());           // insert child value
                writer.WriteEndElement();                   // end child
                writer.WriteStartElement("q2");             // open child / sibling
                writer.WriteString(Q2Feedback());           // insert child / sibling value
                writer.WriteEndElement();                   // end child
                writer.WriteStartElement("q3");             // open child / sibling
                writer.WriteString(Q3Feedback());           // insert child / sibling value
                writer.WriteEndElement();                   // end child
                writer.WriteEndElement();                   // end parent
                writer.WriteEndDocument();                  // end root
                writer.Close();                             // close the XmlWriter machine
            }
        }

        //save data to XML file, after the XML file has already been created
        void SaveDataToXML()
        {
            // get time user pressed button for attribute
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

        // check selected raio button, return value to CreateXML/SaveXML
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

        // return the value of the trackbar to CreateXML/SaveXML
        string Q2Feedback()
        {
            return trackBar1.Value.ToString();
        }

        // return the text in the rich text box CreateXML/SaveXML
        string Q3Feedback()
        {
            return richTextBox1.Text.ToString();
        }
    }
}
