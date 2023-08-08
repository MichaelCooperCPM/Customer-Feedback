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
            // load the XML file and create a reference to all the nodes called feedbackNodes which is passed to each function
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("feedback.xml");
            XmlNodeList feedbackNodes = xDoc.SelectNodes("all_feedback/feedback");

            // change the text on each label to the output of the method
            label2.Text = Q1PercentageServeTime(feedbackNodes, "lessThan10").ToString() + "%";
            label4.Text = Q1PercentageServeTime(feedbackNodes, "upTo1").ToString() + "%";
            label6.Text = Q1PercentageServeTime(feedbackNodes, "upto2").ToString() + "%";
            label8.Text = Q1PercentageServeTime(feedbackNodes, "over2").ToString() + "%";

            // change the text on label3 to the output of the method
            label3.Text = Q2StaffAverageScores(feedbackNodes).ToString();

            // call method, passed nodes, to display all feedback in thte dataviewGrid
            Q3DisplayResponses(feedbackNodes);
        }

        // calc percentage of each time period taken to serve customer
        double Q1PercentageServeTime(XmlNodeList feedbackNodes, string timePassed)
        {
            double serveTimecount = 0;
            double allCount = 0;
            double percentage;

            foreach (XmlNode xNode in feedbackNodes)
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
        double Q2StaffAverageScores(XmlNodeList feedbackNodes)
        {
            // create a list to store the values, better than using an array as the size of the list does
            // not need to be predetermined, it will increase based on the number of q2 elements in the XML file
            List<double> staffScoresList = new List<double>();

            // get the values from each q2 element and add them into the list
            foreach (XmlNode xNode in feedbackNodes)
            {
                staffScoresList.Add(double.Parse(xNode.SelectSingleNode("q2").InnerText));
            }

            // find the average of the values in the list (Average() requires System.Linq)
            double vAverage = staffScoresList.Average();

            return Math.Round(vAverage,1);
        }

        // display date_time attribute and each q's elements in the datagridview object
        void Q3DisplayResponses(XmlNodeList feedbackNodes)
        {
            foreach (XmlNode xNode in feedbackNodes)
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
