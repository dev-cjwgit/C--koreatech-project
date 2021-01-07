using koreatech_bachelor_Post_Project.Binding.ObjectViewModel;
using Microsoft.VisualBasic;
using ProgramCore;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WinHttp;

namespace koreatech_bachelor_Post_Project.Windows.Pages
{
    /// <summary>
    /// PostPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PostPage : UserControl
    {
        // 70, 0, 115, 190, 70 
        private double[] basket_auto_size = { 70, 0, 115, 90, 70 };
        private WinHttpRequest winhttp;
        private string url;
        public PostPage()
        {
            InitializeComponent();
            winhttp = new WinHttpRequest();
            PostListView.ItemsSource = PostListViewModel.GetInstance();
            MaxPageText.DataContext = PageViewModel.GetInstance();
            PresentPageText.DataContext = PageViewModel.GetInstance();
            // 학사 공지사항을 default로 설정 //
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/bachelorList.do";
            PostListModel.SetSource(GetPostList(url, 1));
        }

        private List<PostListEntity> GetPostList(string url, int page)
        {
            try
            {
                winhttp.Open("GET", url + "?page=" + page);
                winhttp.Send();
                winhttp.WaitForResponse();
                string temp = Strings.Split(winhttp.ResponseText, "<th scope=\"col\">조회수</th>")[1]; ;
                int total_post = Convert.ToInt32(Strings.Split(Strings.Split(winhttp.ResponseText, "총 <em>")[1], "</em>")[0]); // 총 게시글 수
                int total_pages = (int)(total_post / 20.0 + 0.999); // 총 페이지 수
                PageViewModel.GetInstance().MaxPage = total_pages;
                int notice_size = Strings.Split(temp, "alt=\"공지\"").Length - 1; // 공지사항 개수
                int post_size = Strings.Split(temp, "<td class=\"subject\">").Length - 1; // 페이지당 개시글 개수
                PostListModel.BoardId = Convert.ToInt32(Strings.Split(Strings.Split(winhttp.ResponseText, "board_id\" value=\"")[1], "\" />")[0]);
                List<PostListEntity> result = new List<PostListEntity>();
                for (int i = 0; i < post_size; i++)
                {
                    int number = Convert.ToInt32(Strings.Split(Strings.Split(temp, "<td class=\"num\">")[(i + 1) * 2], "</td>")[0]);
                    string title = Strings.Split(Strings.Split(temp, "title=\"")[i + 1], "\">")[0];
                    if (i < notice_size)
                    {
                        title = "[공지]" + title;
                    }
                    string time = Strings.Split(Strings.Split(temp, "<td class=\"date\">")[i + 1], "</td>")[0];
                    string publisher = Strings.Split(Strings.Split(temp, "<td class=\"writer\">")[i + 1], "</td>")[0];
                    int views = Convert.ToInt32(Strings.Split(Strings.Split(temp, "<td class=\"cnt\">")[i + 1], "</td>")[0]);

                    result.Add(new PostListEntity()
                    {
                        Number = number,
                        Title = title,
                        Time = time,
                        Publisher = publisher,
                        Views = views
                    });
                }
                return result;

            }
            catch (Exception ex)
            {
                return new List<PostListEntity>();
            }
        }
        private void PageInit()
        {
            PageViewModel.GetInstance().PresentPage = 1;
        }
        private void NormalNoticeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/list.do";
            PostListModel.SetSource(GetPostList(url, 1));
            PageInit();

        }
        private void ScholarshipNoticeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/scholarList.do";
            PostListModel.SetSource(GetPostList(url, 1));
            PageInit();

        }

        private void BachelorNoticeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/bachelorList.do";
            PostListModel.SetSource(GetPostList(url, 1));
            PageInit();

        }

        private void HireNoticeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/boardList10.do";
            PostListModel.SetSource(GetPostList(url, 1));
            PageInit();

        }

        private void CoronaNoticeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            url = "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/boardList8.do";
            PostListModel.SetSource(GetPostList(url, 1));
            PageInit();
        }

        private void PostListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double remainingSpace = PostListView.ActualWidth;
            if (remainingSpace > 0)
            {
                for (int idx = 0; idx < 5; idx++)
                {
                    if (basket_auto_size[idx] > 0)
                    {
                        (PostListView.View as GridView).Columns[idx].Width = Math.Ceiling(basket_auto_size[idx]);
                        remainingSpace -= Math.Ceiling(basket_auto_size[idx]);
                    }
                    else if (basket_auto_size[idx] < 0)
                    {
                        (PostListView.View as GridView).Columns[idx].Width = Math.Ceiling(remainingSpace / basket_auto_size[idx]);
                    }
                }
            } (PostListView.View as GridView).Columns[1].Width = remainingSpace - 23;
        }

        private void PageBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostListModel.PresentPage - 1 > 0)
            {
                PostListModel.PresentPage -= 1;
                PostListModel.SetSource(GetPostList(url, PostListModel.PresentPage));
            }
        }

        private void PageNextButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostListModel.PresentPage + 1 <= PostListModel.MaxPage)
            {
                PostListModel.PresentPage += 1;
                PostListModel.SetSource(GetPostList(url, PostListModel.PresentPage));
            }
        }

        private void PostListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic meta_data = sender as dynamic;
            if (meta_data.SelectedIndex > -1)
            {
                winhttp.Open("GET", "https://www.koreatech.ac.kr/kor/CMS/NoticeMgr/view.do?post_seq=" + PostListModel.DataList[meta_data.SelectedIndex].Number + "&board_id=" + PostListModel.BoardId);
                winhttp.Send();
                winhttp.WaitForResponse();
                PostBodyEntity post = new PostBodyEntity();

                string temp = Strings.Split(Strings.Split(winhttp.ResponseText, "<div id=\"board-wrap\">")[1], "<div  class=\"board-view-more\">")[0];
                string title = Strings.Split(Strings.Split(temp, "<span style=\"\" title=\"")[1], "\">")[0];
                string publisher = Strings.Split(Strings.Split(temp, "<span class=\"txt name\">")[1], "</span>")[0];
                string time = Strings.Split(Strings.Split(temp, "<span class=\"txt\">")[1], "</span>")[0];
                int views = Convert.ToInt32(Strings.Split(Strings.Split(temp, "<em>조회수 : </em>")[1], "</span>")[0]);

                List<Attachments> list = new List<Attachments>();
                if (Strings.InStr(temp, "<span class=\"ilbl\">첨부파일</span>") > 0)
                {
                    for (int i = 0; i < Strings.Split(temp, "<a href=\"").Length - 1; i++)
                    {
                        string save_str = Strings.Split(temp, "<a href=\"")[i + 1];
                        list.Add(new Attachments()
                        {
                            Title = Strings.Split(Strings.Split(save_str, "\">")[1], "</a>")[0],
                            Url = Strings.Split(save_str, "\">")[0]
                        });
                    }
                }
                else
                {
                    list = null;
                }
                string body = Strings.Split(Strings.Split(temp, "<div class=\"board-view-contents\">")[1], "</div>")[0].Trim() + "</div>";

                post.Title = title;
                post.Publisher = publisher;
                post.Time = time;
                post.Views = views;
                post.Attachment = list;
                post.Body = body;

            }
        }
    }
}
