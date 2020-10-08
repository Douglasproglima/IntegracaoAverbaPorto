using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIAverbaPorto
{
    public partial class Form1 : Form
    {
        #region Constantes
        const string BASE_URL = @"https://api.averbeporto.com.br/websys/php/conn.php";
        const string COMP = "5";
        #endregion

        #region Construtor
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string json = this.SendXMLAverbaPorto();
            memoResponse.Text = json;
        }
        #endregion

        #region Métodos
        private string SendXMLAverbaPorto()
        {
            string user = "01375240010104";
            string pass = "0137";
            List<string> cookies = this.GetCookiePortalSES(user, pass);
            string cookiePortalSe = cookies[0];
            string cookieName = cookies[1];
            string response = string.Empty;

            if (!string.IsNullOrEmpty(cookiePortalSe))
            {
                var restResponseCookie = new RestResponseCookie();
                restResponseCookie.Name = cookieName;
                restResponseCookie.Value = cookiePortalSe;

                var upload = new RestClient(BASE_URL);
                upload.Timeout = -1;
                var requestUpload = new RestRequest(Method.POST);

                requestUpload.AddCookie(restResponseCookie.Name, restResponseCookie.Value);
                requestUpload.AddHeader("Content-Type", "multipart/form-data");
                requestUpload.AddParameter("comp", COMP);
                requestUpload.AddParameter("mod", "Upload");
                requestUpload.AddParameter("path", "eguarda/php/");
                requestUpload.AddParameter("dump", "1");
                requestUpload.AddParameter("recipient", ""); //D = Duplo Ramo(As NF-e são averbadas 2 vezes, uma como T e outra como E)
                requestUpload.AddParameter("v", 2);

                string file = @"A:/procCTe_31200819931971000183570010000011591054036608.xml";
                byte[] arry = File.ReadAllBytes(file);
                requestUpload.AddFile("file", arry, "procCTe_31200819931971000183570010000011591054036608.xml", "application/xml");

                IRestResponse responseUpload = upload.Execute(requestUpload);
                Console.WriteLine(responseUpload.Content);

                response = responseUpload.Content;
            }

            return response;
        }

        private List<string> GetCookiePortalSES(string user, string pass)
        {
            var client = new RestClient(BASE_URL);
            var requestLogin = new RestRequest(Method.POST);

            var parametros = string.Format(@"mod=login&comp={0}&user={1}&pass={2}", COMP, user, pass);
            requestLogin.AddHeader("cache-control", "no-cache");
            requestLogin.AddHeader("content-type", "application/x-www-form-urlencoded");
            requestLogin.AddParameter("application/x-www-form-urlencoded", parametros, ParameterType.RequestBody);

            IRestResponse login = client.Execute(requestLogin);

            List<string> cookiePortalSe = new List<string>();
            if (login.StatusCode == HttpStatusCode.OK)
            {
                cookiePortalSe.Add(login.Cookies[0].Value);
                cookiePortalSe.Add(login.Cookies[0].Name);
            }

            return cookiePortalSe;
        }

        #endregion
    }
}
