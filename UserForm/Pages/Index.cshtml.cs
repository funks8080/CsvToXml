using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UserForm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string CSV = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"question1.csv");
        public List<UserModel> UserList { get; set; } = new List<UserModel>();
        public bool IsActive { get; set; }
        public string State { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            UserList = GetUserList();
        }

        public void OnGet()
        {

        }

        public void OnPostSubmit(UserModel user)
        {
            Console.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}",user.FirstName,user.LastName,user.Address,user.City,user.State,user.Zip,user.IsActive));
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"question1.csv");
            if (!System.IO.File.Exists(CSV))
            {
                Console.WriteLine("NO FILE");
            }
            using (StreamWriter sw = System.IO.File.AppendText(CSV))
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",{5},{6}", user.FirstName, user.LastName, user.Address, user.City, user.State, user.Zip, user.IsActive));
            }

            UserList = GetUserList();
        }

        private List<UserModel> GetUserList()
        {
            var header = true;
            var userList = new List<UserModel>();
            using (TextFieldParser parser = new TextFieldParser(CSV))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    if (header)
                    {
                        header = false;
                        continue;
                    }
                    userList.Add(new UserModel()
                    {
                        FirstName = fields[0],
                        LastName = fields[1],
                        Address = fields[2],
                        City = fields[3],
                        State = fields[4],
                        Zip = fields[5],
                        IsActive = bool.Parse(fields[6])
                    });
                }
            }
            return userList;
        }

        public Dictionary<string, string> StateDic = new Dictionary<string, string>() {
            {"Alabama","AL"},
            {"Alaska","AK"},
            {"Arizona","AZ"},
            {"Arkansas","AR"},
            {"California","CA"},
            {"Colorado","CO"},
            {"Connecticut","CT"},
            {"Delaware","DE"},
            {"Florida","FL"},
            {"Georgia","GA"},
            {"Hawaii","HI"},
            {"Idaho","ID"},
            {"Illinois","IL"},
            {"Indiana","IN"},
            {"Iowa","IA"},
            {"Kansas","KS"},
            {"Kentucky","KY"},
            {"Louisiana","LA"},
            {"Maine","ME"},
            {"Maryland","MD"},
            {"Massachusetts","MA"},
            {"Michigan","MI"},
            {"Minnesota","MN"},
            {"Mississippi","MS"},
            {"Missouri","MO"},
            {"Montana","MT"},
            {"Nebraska","NE"},
            {"Nevada","NV"},
            {"New Hampshire","NH"},
            {"New Jersey","NJ"},
            {"New Mexico","NM"},
            {"New York","NY"},
            {"North Carolina","NC"},
            {"North Dakota","ND"},
            {"Ohio","OH"},
            {"Oklahoma","OK"},
            {"Oregon","OR"},
            {"Pennsylvania","PA"},
            {"Rhode Island","RI"},
            {"South Carolina","SC"},
            {"South Dakota","SD"},
            {"Tennessee","TN"},
            {"Texas","TX"},
            {"Utah","UT"},
            {"Vermont","VT"},
            {"Virginia","VA"},
            {"Washington","WA"},
            {"West Virginia","WV"},
            {"Wisconsin","WI"},
            {"Wyoming","WY"}

            //{"AL","Alabama"},
            //{"AK","Alaska"},
            //{"AZ","Arizona"},
            //{"AR","Arkansas"},
            //{"CA","California"},
            //{"CO","Colorado"},
            //{"CT","Connecticut"},
            //{"DE","Delaware"},
            //{"FL","Florida"},
            //{"GA","Georgia"},
            //{"HI","Hawaii"},
            //{"ID","Idaho"},
            //{"IL","Illinois"},
            //{"IN","Indiana"},
            //{"IA","Iowa"},
            //{"KS","Kansas"},
            //{"KY","Kentucky"},
            //{"LA","Louisiana"},
            //{"ME","Maine"},
            //{"MD","Maryland"},
            //{"MA","Massachusetts"},
            //{"MI","Michigan"},
            //{"MN","Minnesota"},
            //{"MS","Mississippi"},
            //{"MO","Missouri"},
            //{"MT","Montana"},
            //{"NE","Nebraska"},
            //{"NV","Nevada"},
            //{"NH","New Hampshire"},
            //{"NJ","New Jersey"},
            //{"NM","New Mexico"},
            //{"NY","New York"},
            //{"NC","North Carolina"},
            //{"ND","North Dakota"},
            //{"OH","Ohio"},
            //{"OK","Oklahoma"},
            //{"OR","Oregon"},
            //{"PA","Pennsylvania"},
            //{"RI","Rhode Island"},
            //{"SC","South Carolina"},
            //{"SD","South Dakota"},
            //{"TN","Tennessee"},
            //{"TX","Texas"},
            //{"UT","Utah"},
            //{"VT","Vermont"},
            //{"VA","Virginia"},
            //{"WA","Washington"},
            //{"WV","West Virginia"},
            //{"WI","Wisconsin"},
            //{ "WY","Wyoming"}
        };


    }
}
