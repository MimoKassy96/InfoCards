using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Client.Service
{
    public class InfoCardsService : IInfoCardsService
    {
        private readonly ILogger<InfoCardsService> _logger;

        public InfoCardsService(ILogger<InfoCardsService> logger)
        {
            _logger = logger;
        }

        public async Task<List<InfoCardModel>> GetInfoAsync()
        {
            try
            {
                var result = new List<InfoCardModel>();
                JsonTextReader reader = null;
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var response = await client.GetAsync("https://localhost:44338/card/getInfocards");
                    response.EnsureSuccessStatusCode();

                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = new StreamReader(await response.Content.ReadAsStreamAsync()))
                        {
                            reader = new JsonTextReader(stream);
                            result = new Newtonsoft.Json.JsonSerializer().Deserialize<List<InfoCardModel>>(reader);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't get info cards! Error: {ex}");
                MessageBox.Show("Info cards not downloaded, error!");
                throw;
            }
        }

        public async Task<BitmapImage> UploadImage(string id)
        {
            try
            {
                var result = new BitmapImage();
                var url = ("https://localhost:44338/card/getImage?id=" + $"{id}");
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            var bitmap = new BitmapImage();

                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = stream;
                            bitmap.EndInit();
                            bitmap.Freeze();
                            result = bitmap;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't upload image! Error: {ex}");
                MessageBox.Show("Image not uploaded, error!");
                throw;
            }
        }

        public async Task<List<InfoCardModel>> DeleteInfoCard(string id)
        {
            try
            {
                JsonTextReader reader = null;
                var result = new List<InfoCardModel>();
                var url = ("https://localhost:44338/card/deleteCard?id=" + $"{id}");
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var response = await client.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();

                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = new StreamReader(await response.Content.ReadAsStreamAsync()))
                        {
                            reader = new JsonTextReader(stream);
                            result = new Newtonsoft.Json.JsonSerializer().Deserialize<List<InfoCardModel>>(reader);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't delete card! Error: {ex}");
                MessageBox.Show("Info card not deleted, error!");
                throw;
            }
        }

        public async void AddCard(AddCardParams param) 
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    using (var stream = File.Open(param.path, FileMode.Open))
                    {
                        var fileStreamContent = new StreamContent(stream);
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                        multipartFormContent.Add(fileStreamContent, name: "uploadedFile", fileName: param.fileName);
                        multipartFormContent.Add(new StringContent(Guid.NewGuid().ToString()), name: "id");
                        multipartFormContent.Add(new StringContent(param.info), name: "info");

                        using (var client = new HttpClient(clientHandler))
                        {
                            var response = new HttpResponseMessage();

                            response = await client.PostAsync("https://localhost:44338/card/addCard", multipartFormContent);

                            response.EnsureSuccessStatusCode();
                            await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Info card added");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't add card! Error: {ex}");
                MessageBox.Show("Info card not added, error!");
            }
        }
        public async void UpadateCard(UpadateCardParams param) 
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    using (var stream = File.Open(param.path, FileMode.Open))
                    {
                        var fileStreamContent = new StreamContent(stream);
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                        multipartFormContent.Add(fileStreamContent, name: "uploadedFile", fileName: param.fileName);
                        multipartFormContent.Add(new StringContent(param.id), name: "id");
                        multipartFormContent.Add(new StringContent(param.info), name: "info");

                        using (var client = new HttpClient(clientHandler))
                        {
                            var response = new HttpResponseMessage();

                            response = await client.PutAsync("https://localhost:44338/card/updateCard", multipartFormContent);

                            response.EnsureSuccessStatusCode();
                            await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Info card updated");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't update card! Error: {ex}");
                MessageBox.Show("Info card not updated, error!");
            }
        }
    }
}
