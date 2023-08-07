using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace Customer_Feedback
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            // calculate and display q1 percentages for each serve time
            label2.Text = q1PercentageServeTime("lessThan10").ToString() + "%";
            label4.Text = q1PercentageServeTime("upTo1").ToString() + "%";
            label6.Text = q1PercentageServeTime("upto2").ToString() + "%";
            label8.Text = q1PercentageServeTime("over2").ToString() + "%";

            // calc and display q2 staff service score
            label3.Text = q2StaffAverageScores().ToString();

            // display responses to q3
            q3DisplayResponses();
        }

        // calc percentage of each time period taken to serve customer
        double q1PercentageServeTime(string timePassed)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("feedback.xml");

            double serveTimecount = 0;
            double allCount = 0;
            double percentage;

            foreach (XmlNode xNode in xDoc.SelectNodes("all_feedback/feedback"))
            {
                allCount++; // calc the number of q1 elements

                if (xNode.SelectSingleNode("q1").InnerText == timePassed)
                {
                    serveTimecount++; // increase if equal to the passed serve time
                }
            }

            // calc the percentage for the passed serve time
            percentage = (serveTimecount / allCount) * 100;

            return percentage;
        }

        // calc scores for q2
        double q2StaffAverageScores()
        {
            // create a list to store the values, better than using an array as the size of the list does
            // not need to be predetermined, it will increase based on the number of q2 elements in the XML file
            List<double> staffScoresList = new List<double>();

            //load the XML file
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("feedback.xml");

            // get the values from each q2 element in the XML file and add them into the list
            foreach (XmlNode xNode in xDoc.SelectNodes("all_feedback/feedback"))
            {
                staffScoresList.Add(double.Parse(xNode.SelectSingleNode("q2").InnerText));
            }

            // find the average of the values in the list (Average() requires System.Linq)
            double vAverage = staffScoresList.Average();

            return Math.Round(vAverage,1);
        }

        void q3DisplayResponses()
        {
            //load the XML file
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("feedback.xml");

            // display date_time attribute and each q's elements in the datagridview object
            foreach (XmlNode xNode in xDoc.SelectNodes("all_feedback/feedback"))
            {
                string dateTime = xNode.Attributes["date_time"].Value;
                string serveTime = xNode.SelectSingleNode("q1").InnerText;
                string staffScore = xNode.SelectSingleNode("q2").InnerText;
                string feedback = xNode.SelectSingleNode("q3").InnerText;
                dataGridView1.Rows.Add(dateTime, serveTime, staffScore, feedback); 
            }
        }
    }
}
