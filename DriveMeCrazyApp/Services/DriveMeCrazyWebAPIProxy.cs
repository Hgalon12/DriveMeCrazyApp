﻿using DriveMeCrazyApp.Models;
//using Org.Apache.Http.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//
//using DriveMeCrazyApp.Models;

namespace DriveMeCrazyApp.Services
{
    public class DriveMeCrazyWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "ls94pz8z-5243.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://ls94pz8z-5243.euw.devtunnels.ms/api/";
        public static string ImageBaseAddress = "https://ls94pz8z-5243.euw.devtunnels.ms";
        #endregion

        public DriveMeCrazyWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return DriveMeCrazyWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{DriveMeCrazyWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }



        public async Task<TableUser?> LoginAsync(Login userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    TableUser? result = JsonSerializer.Deserialize<TableUser>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TableUser?> Register(TableUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}register";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    TableUser? result = JsonSerializer.Deserialize<TableUser>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<TableCar?> RegisterCar(TableCar car)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerCar";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(car);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    TableCar? result = JsonSerializer.Deserialize<TableCar>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TableUser?> UploadProfileImage(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}uploadprofileimage";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    TableUser? result = JsonSerializer.Deserialize<TableUser>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<TableCar?> UploadCarImage(string imagePath, string idCar)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}uploadcarimage?carId={idCar}";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    TableCar? result = JsonSerializer.Deserialize<TableCar>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> UpdateUser(TableUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateprofile";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<RequestCar?>requestCar(RequestCar car)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}requestCar";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(car);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    RequestCar? result = JsonSerializer.Deserialize<RequestCar>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }







        public async Task<List<TableCar>> GetAllCarByDriver()
        {
            try
            {
                string url = $"{baseUrl}GetAllCars"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<TableCar>>(resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<TableCar>> GetAllCar()
        {
            try
            {
                string url = $"{baseUrl}GetAllCars"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<TableCar>>(resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<List<TableUser>> GetAllUser()
        {
            try
            {
                string url = $"{baseUrl}GetAllUserByOwner"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<TableUser>>(resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<RequestCar>> GetAllRequest()
        {
            try
            {
                string url = $"{baseUrl}GetAllRequestPannding"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<RequestCar>>(resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<RequestCar>> GetAllRequest12()
        {
            try
            {
                string url = $"{baseUrl}GetAllRequestPanndingAndAprove"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<RequestCar>>(resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }





        public async Task<ChoreType?> AddChore(ChoreType chore)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}AddChore";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(chore);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ChoreType? result = JsonSerializer.Deserialize<ChoreType>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> ChangeRestStatusToApproved(RequestCar r)
        {
            string url = $"{this.baseUrl}ChangeStatusRequestToAprrove";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(r);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    bool result = JsonSerializer.Deserialize<bool>(resContent, options);
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> ChangeRestStatusToReject(RequestCar request)
        {
            string url = $"{this.baseUrl}ChangeStatusRequestToRegject";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(request);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    bool result = JsonSerializer.Deserialize<bool>(resContent, options);
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task <DriverCar?> AddDriver(string carId, int userId)
        {
            try
            {
                string url = $"{baseUrl}adddriver?carId={carId}&userId={userId}"; // Endpoint של ה-API לקבלת כל המכוניות
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize < DriverCar > (resContent, options);
                }
                else
                {
                    return null; // או להחזיר רשימה ריקה, תלוי איך אתה רוצה לטפל בכישלון
                }
            }
            catch (Exception)
            {
                return null;
            }
        }





        public async Task<List<CarUseChart>?> GetCarUsage(int? parentId, int days)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getCarUsage?parentId={parentId}&days={days}";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {

                    // המרת התוכן שנקבל לאובייקטים
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<CarUseChart>? output = JsonSerializer.Deserialize<List<CarUseChart>>(resContent, options);
                    return output;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }












    }
}
