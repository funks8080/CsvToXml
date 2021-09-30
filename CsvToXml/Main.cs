using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

namespace CsvToXml
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var file = txtFile.Text;

            try
            {
                CheckFile(file);

                var directory = Path.GetDirectoryName(file);
                var fileName = string.Format("results-{0}.xml", Path.GetFileNameWithoutExtension(file));
                var xmlFile = Path.Combine(directory, fileName);
                var userList = InputFile(file);

                if (userList.Count < 1)
                {
                    MessageBox.Show("No data found.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlSerializer serializer = new XmlSerializer(userList.GetType());
                using (FileStream fs = File.Create(xmlFile))
                {
                    serializer.Serialize(fs, userList);
                }

                linkFile.Text = xmlFile;
                linkFile.Links.Clear();
                linkFile.Links.Add(0, xmlFile.Length - 1, xmlFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckFile(string file)
        {
            if (file.EndsWith(".csv"))
                return true;

            throw new Exception("Please choose a .CSV file");
        }

        private Users InputFile(string file)
        {
            var userList = new Users();

            using (var reader = new StreamReader(file))
            {
                var firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }   
                            

                    var lineSplit = line.Split(',');
                    if (lineSplit.Length != 7)
                    {
                        Console.WriteLine("Invalid Length: " + string.Join(",", lineSplit));
                    }

                    userList.Add(new User()
                    {
                        FirstName = lineSplit[0],
                        LastName = lineSplit[1],
                        Address = lineSplit[2],
                        City = lineSplit[3],
                        State = lineSplit[4],
                        Zip = lineSplit[5],
                        Active = bool.Parse(lineSplit[6])
                    });
                }
            }
            return userList;

        }

        private DynamicClass InputDynamicFile(string file)
        {
            var unknownData = new DynamicClass();
            using (var reader = new StreamReader(file))
            {
                var firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (firstLine)
                    {
                        unknownData.Headers = line.Split(',').ToList();
                        firstLine = false;
                    }

                    unknownData.Entries.Add(new DynamicClass.Entry() { Values = line.Split(',').ToList() });
                }
            }
            return unknownData;
        }

        private void linkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
