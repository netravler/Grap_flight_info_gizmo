using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace Grap_flight_info_gizmo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // examples
            // http://stackoverflow.com/questions/10826260/is-there-a-way-to-read-from-a-website-one-line-at-a-time
            // http://social.msdn.microsoft.com/Forums/en-US/1c4cb854-6e32-4d60-be20-bb8ef0218d45/how-can-i-add-lines-in-a-richtextbox-in-c-?forum=winforms
            
            //try  // pull data from c:\airportdata\part139.txt csv from FAA.org and parse...
            //{
                 // Open the stream and read it back. 
                string airportData;

                System.IO.StreamReader Airport = new System.IO.StreamReader(@textBox1.Text);
                
                while ((airportData = Airport.ReadLine()) != null)
                {
                        getTheFlightHistory(airportData);
                }
            //}
            //catch
            //{
            //    richTextBox1.AppendText("ERRORERRORERRORERRORERRORERRORERRORERRORERRORERRORERRORERRORERRORERROR");
            //}
        }

        private void getTheFlightHistory(String Airport)
        {
            /*
             * Ok in a nutshell we are going to read down through the stream of data and only retain the following lines:
             * <td><span title="
             * <td class="nowrap" itemscope itemtype="
             * <span class="tz">
             * 
             * these lines will provide for the only content we will need...
             * 
             * code example:
             * var url ="http://flightaware.com/live/airport/KMCI";
               var client = new WebClient();
               using (var stream = client.OpenRead(url))
               using (var reader = new StreamReader(stream))
               {
                    string line;
                
                    while ((line = reader.ReadLine()) != null)
                    {
                        // do stuff
                    }
                }
             */

            // strings to check

            string span = @"<td><span title=";
            string td = @"<td class=""nowrap"" itemscope itemtype=";
            string classtz = @"tz";

            var url = "http://flightaware.com/live/airport/" + Airport.Trim();
            var client = new WebClient();
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    // do stuff
                    if (line.Contains(span) | line.Contains(td) | line.Contains(classtz))
                    {
                        richTextBox1.AppendText(line + "\n");
                    }
                }
            }

            // close this or it will ball-up
         }


        public static string[] GetStringInBetween(string strBegin, string strEnd, string strSource, bool includeBegin, bool includeEnd)
        {

            string[] result = { "", "" };

            int iIndexOfBegin = strSource.IndexOf(strBegin);

            if (iIndexOfBegin != -1)
            {

                // include the Begin string if desired

                if (includeBegin)

                    iIndexOfBegin -= strBegin.Length;

                strSource = strSource.Substring(iIndexOfBegin

                    + strBegin.Length);

                int iEnd = strSource.IndexOf(strEnd);

                if (iEnd != -1)
                {

                    // include the End string if desired

                    if (includeEnd)

                        iEnd += strEnd.Length;

                    result[0] = strSource.Substring(0, iEnd);

                    // advance beyond this segment

                    if (iEnd + strEnd.Length < strSource.Length)

                        result[1] = strSource.Substring(iEnd

                            + strEnd.Length);

                }

            }

            else

                // stay where we are

                result[1] = strSource;

            return result;

        }

        public static string RemoveBetween(string strBegin, string strEnd, string strSource, bool removeBegin, bool removeEnd)
        {

            string[] result = GetStringInBetween(strBegin, strEnd, strSource, removeBegin, removeEnd);

            if (result[0] != "")
            {

                return strSource.Replace(result[0], "");

            }

            // nothing found between begin & end

            return strSource;

        }

        public static string removeStringStuff(string edithold, string keepstring)
        {
            string newbigline = "";

            foreach (char spyme in edithold)
            {
                if (keepstring.Contains(spyme))
                {
                    newbigline += spyme;
                }
            }

            return newbigline;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        /* Jack Henry and Associates Jets
                 
               http://flightaware.com/live/flight/N894JH
               http://flightaware.com/live/flight/N156JH
               http://flightaware.com/live/flight/N152JH
               http://flightaware.com/live/flight/N157JH
               http://flightaware.com/live/flight/N156JH

           */
    }


 }

