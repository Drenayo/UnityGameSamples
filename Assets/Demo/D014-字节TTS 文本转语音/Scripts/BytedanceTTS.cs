using System.Collections;
using UnityEngine;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

namespace D014
{
    public class BytedanceTTS : MonoBehaviour
    {
        public string token = "";
        public string appid = "";
        public string uid = "";

        [Space(10)]
        public InputField inputField;
        public Button btn_Start;

        private string url = "https://openspeech.bytedance.com/api/v1/tts";
        private string path = "Demo/D014-字节TTS 文本转语音/Audio";
        private string textStr = "";
        private string fullPath;

        private void Start()
        {
            btn_Start.onClick.AddListener(Btn_TTS);

            fullPath = Application.dataPath + "/" + path + "/a.wav";
            Debug.Log("存放路径：" + fullPath);
        }

        private void OnDestroy()
        {
            btn_Start.onClick.RemoveListener(Btn_TTS);
        }

        // 按钮触发事件 TTS
        private void Btn_TTS()
        {
            textStr = inputField.text;
            _ = Task.Run(async () =>
            {
                await StartTTSAsync();
            });
        }

        public async Task StartTTSAsync()
        {
            using (HttpClient client = new HttpClient())
            {

                // 添加请求头 | 鉴权验证
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $";{token}");

                // 构建请求体
                var requestBody = new PostRequestBody
                {
                    app = new App
                    {
                        appid = appid,
                        token = token,
                        cluster = "volcano_tts"
                    },
                    user = new User
                    {
                        uid = uid,
                    },
                    audio = new Audio
                    {
                        voice_type = "BV700_V2_streaming",
                        encoding = "wav",
                        speed_ratio = 1.0,
                        volume_ratio = 1.0,
                        pitch_ratio = 1.0
                    },
                    request = new Request
                    {
                        reqid = Guid.NewGuid().ToString(),
                        text = textStr,
                        text_type = "plain",
                        operation = "query",
                        with_frontend = 1,
                        frontend_type = "unitTson"
                    }
                };

                StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonContent);
                string responseData = response.Content.ReadAsStringAsync().Result;
                // 将JSON响应解析为JObject 
                JObject jsonResponse = JObject.Parse(responseData);

                if (response.IsSuccessStatusCode)
                {
                    // 解析指定字段 
                    JToken dataToken = jsonResponse["data"];

                    if (dataToken != null)
                    {
                        // 解码 
                        byte[] bytes = Convert.FromBase64String(dataToken.ToString());
                        File.WriteAllBytes(fullPath, bytes);
                        Debug.Log($"生成语音【{textStr}】成功！");
                    }
                }
                else
                {
                    Debug.Log($"错误原因：{jsonResponse["message"]}");
                }
            }
        }
    }

    public class App
    {
        public string appid;
        public string token;
        public string cluster;
    }
    public class User
    {
        public string uid;
    }
    public class Audio
    {
        public string voice_type;
        public string encoding;
        public double speed_ratio;
        public double volume_ratio;
        public double pitch_ratio;
    }
    public class Request
    {
        public string reqid;
        public string text;
        public string text_type;
        public string operation;
        public int with_frontend;
        public string frontend_type;
    }
    public class PostRequestBody
    {
        public App app;
        public User user;
        public Audio audio;
        public Request request;
    }
}