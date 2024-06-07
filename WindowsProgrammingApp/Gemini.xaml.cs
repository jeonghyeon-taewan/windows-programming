using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MarkdownSharp;
using Markdig.Syntax;

namespace WindowsProgrammingApp
{
    public partial class Gemini : Window
    {
        public Gemini()
        {
            InitializeComponent();
        }

        private async void SendPostRequest()
        {
            // 버튼을 로딩 상태로 변경
            CopyTextButton.IsEnabled = false;
            CopyTextButton.Content = "Loading...";

            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=AIzaSyAQicF6fXLQb6Q0TV8QHMHEAHs6S9bWfyM";
            string jsonBody = "{\"contents\":[{\"parts\":[{\"text\":\"" + InputTextBlock.Text + "\"}]}]}";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        string extractedText = ExtractTextFromResponse(responseBody);
                        DisplayBoldText(extractedText);
                        //string plainText = MarkdownParser.ParseMarkdownToPlainText(extractedText);
                        //Paragraph paragraph = new Paragraph();
                        //paragraph.Inlines.Add(plainText);
                        //OutputRichTextBox.Document.Blocks.Add(paragraph);
              
                    }
                    else
                    {
                        MessageBox.Show("Error: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
                finally
                {
                    // 응답 처리 후 버튼을 다시 활성화하고, 원래의 텍스트로 변경
                    CopyTextButton.IsEnabled = true;
                    CopyTextButton.Content = "Search";
                }
            }
        }


        private string ExtractTextFromResponse(string jsonResponse)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                var text = jsonObject["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                return text ?? "No text found";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing JSON: " + ex.Message);
                return "Error";
            }
        }

        private void DisplayBoldText(string text)
        {
            OutputRichTextBox.Document.Blocks.Clear();
            if (string.IsNullOrEmpty(text))
            {
                OutputRichTextBox.Document.Blocks.Add(new Paragraph(new Run("No text found")));
                return;
            }



            string[] parts = text.Split(new[] { "**" }, StringSplitOptions.None);
            bool isBold = false;
            Paragraph paragraph = new Paragraph();
            foreach (var part in parts)
            {
                Run runText = new Run(part)
                {
                    FontSize = 16 // 기본 텍스트 크기
                };

                if (isBold)
                {
                    runText.FontWeight = FontWeights.Bold;
                }

                paragraph.Inlines.Add(runText);
                isBold = !isBold;
            }

            OutputRichTextBox.Document.Blocks.Add(paragraph);
        }

        private void CopyTextButton_Click(object sender, RoutedEventArgs e)
        {
            SendPostRequest();
        }
    }
    public class MarkdownParser
    {
        public static string ParseMarkdownToPlainText(string markdown)
        {
            var markdownTransformer = new Markdown();
            string html = markdownTransformer.Transform(markdown);

            // HTML에서 텍스트만 추출 (HtmlAgilityPack 사용 권장)
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            string plainText = htmlDoc.DocumentNode.InnerText;

            return plainText;
        }
    }
}
