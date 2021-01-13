using Microsoft.VisualBasic;
using System;
using System.Windows;
using System.Windows.Controls;
using WinHttp;

namespace koreatech_bachelor_Post_Project.Windows.Pages
{
    /// <summary>
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WinHttpRequest winhttp = new WinHttpRequest();
            winhttp.Open("POST", "https://portal.koreatech.ac.kr/sso/sso_login.jsp");
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            winhttp.Send("user_id=id&user_pwd=pw&RelayState=%2Findex.jsp&id=PORTAL&targetId=PORTAL");
            winhttp.WaitForResponse();
            string WMONID = Strings.Split(Strings.Split(winhttp.GetAllResponseHeaders(), "Set-Cookie: ")[1], "; ")[0];
            string JSessionID = Strings.Split(Strings.Split(winhttp.GetAllResponseHeaders(), "Set-Cookie: ")[2], " Path")[0];

            winhttp.Open("POST", "https://portal.koreatech.ac.kr/ktp/login/checkLoginId.do");
            winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            winhttp.SetRequestHeader("Cookie", "WMONID=" + WMONID + "; JSESSIONID=" + JSessionID + " setNowLoginId=id");
            winhttp.Send("login_id=id&login_pwd=pw&login_type=&login_empno=&login_certDn=&login_certChannel=");
            winhttp.WaitForResponse();
            if (Strings.Split(Strings.Split(winhttp.ResponseText, "\"result\":\"")[1], "\"")[0] == "true")
            {
                int cookie_cnt = Strings.Split(winhttp.GetAllResponseHeaders(), "Set-Cookie: ").Length - 1;
                string[] cookies = new string[cookie_cnt];
                for (int i = 0; i < cookie_cnt; i++)
                {
                    cookies[i] = Strings.Split(Strings.Split(winhttp.GetAllResponseHeaders(), "Set-Cookie: ")[i + 1], " Domain")[0];
                }
                winhttp.Open("POST", "https://portal.koreatech.ac.kr/ktp/login/checkSecondLoginCert.do");
                winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                string temp_cookie = WMONID + "; " + JSessionID;
                string re = temp_cookie;
                for (int i = 0; i < cookie_cnt; i++)
                {
                    temp_cookie += " " + cookies[i];
                }
                winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                winhttp.SetRequestHeader("Cookie", temp_cookie);

                winhttp.Send("login_id=id");
                winhttp.WaitForResponse();
                re += " setNowLoginId=id;";
                for (int i = 0; i < cookie_cnt; i++)
                {
                    re += " " + cookies[i];
                }
                //re += " kut_login_type=id; Domain=koreatech.ac.kr; Path=/; hostOnly=false;";
                re += " kut_login_type=id;";

                winhttp.Open("POST", "https://portal.koreatech.ac.kr/exsignon/sso/sso_assert.jsp");
                //winhttp.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                //winhttp.SetRequestHeader("Accept-Encoding","gzip, deflate, br");
                //winhttp.SetRequestHeader("Accept-Language","ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7");
                //winhttp.SetRequestHeader("Cache-Control","max-age=0");
                //winhttp.SetRequestHeader("Connection","keep-alive");
                //winhttp.SetRequestHeader("Content-Length","73");
                winhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                winhttp.SetRequestHeader("Cookie", re);
                //winhttp.SetRequestHeader("Host","portal.koreatech.ac.kr");
                //winhttp.SetRequestHeader("Origin", "https://portal.koreatech.ac.kr");
                //winhttp.SetRequestHeader("Referer","https://portal.koreatech.ac.kr/sso/sso_login.jsp");
                //winhttp.SetRequestHeader("Sec-Fetch-Dest","document");
                //winhttp.SetRequestHeader("Sec-Fetch-Mode","navigate");
                //winhttp.SetRequestHeader("Sec-Fetch-Site","same-origin");
                //winhttp.SetRequestHeader("Upgrade-Insecure-Request","1");
                //winhttp.SetRequestHeader("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
                winhttp.Send("certUserId=&certLoginId=&certEmpNo=&certType=&secondCert=&langKo=&langEn=");
                winhttp.WaitForResponse();
                Console.WriteLine();
            }
        }
    }
}
