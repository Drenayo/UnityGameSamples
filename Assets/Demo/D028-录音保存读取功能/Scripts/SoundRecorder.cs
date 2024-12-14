using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SoundRecorder : MonoBehaviour
{
    // 声音剪辑
    private AudioClip clip; 
    // 声音播放组件
    private AudioSource aud;
    // 当前使用的麦克风名字
    private string microphoneName;
    // 麦克风列表的字符串
    private string[] micDevicesNames;
    // 是否开始录音
    public bool isRecord = false;
    // 麦克风是否连接
    public bool micConnected = false;
    // 提示UI
    public Text infoText;

    // Start is called before the first frame update
    private void Start()
    {
        // 获取麦克风列表
        micDevicesNames = Microphone.devices;
        // 获取播放声音的组件
        aud = GetComponent<AudioSource>();
        if (micDevicesNames.Length <= 0)
        {
            ShowInfo("缺少麦克风设备");
            Debug.LogWarning("缺少麦克风设备");
        }
        else
        {
            microphoneName = micDevicesNames[0];
            micConnected = true;
            ShowInfo("当前用户录音的麦克风名字为：" + micDevicesNames[0]);
        }
    }


    private void ShowInfo(string info)
    {
        infoText.gameObject.SetActive(true);
        infoText.text = info;
        Invoke("HideInfo", 3f);
    }
    private void HideInfo()
    {
        infoText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 开始录音
    /// </summary>
    public void Begin()
    {
        if (micConnected)
        {
            if (!Microphone.IsRecording(null))
            {
                clip = Microphone.Start(microphoneName, false, 60, 44100);
                ShowInfo("录音已开始");
                isRecord = true;
            }
            else
            {
                ShowInfo("正在录音中！");
            }
        }
        else
        {
            ShowInfo("请确认麦克风设备是否已连接！");
        }
    }


    /// <summary>
    /// 停止录音
    /// </summary>
    public void Stop()
    {
        Microphone.End(null);
        isRecord = false;
        ShowInfo("录音结束");
    }


    /// <summary>
    /// 播放录音
    /// </summary>
    public void Player()
    {
        if (!Microphone.IsRecording(null))
        {
            aud.clip = clip;
            aud.Play();
            ShowInfo("正在播放录音..");
        }
        else
        {
            ShowInfo("正在录音中，请先停止录音！");
        }
    }


    // 保存录音
    public void Save()
    {
        if (!Microphone.IsRecording(null))
        {
            // 将音频转化为字节数组
            byte[] data = GetRealAudio(aud.clip);
            // 录音保存的名字
            string fileName = "example"; //DateTime.Now.ToString("yyyyMMddHHmmss");
            //如果不是“.wav”格式的，加上后缀
            if (!fileName.ToLower().EndsWith(".wav"))
            {
                fileName += ".wav";
            }
            // Path.Combine 拼接字符串
            string path = Path.Combine(Application.persistentDataPath, fileName);//录音保存路径
                                                                                  //输出路径
            // 创建一个空的流文件，以便将音频数据写入
            using (FileStream fs = CreateEmpty(path))
            {
                //wav头文件
                WriteHeader(fs, aud.clip);
                // 将音频字符全部写入流文件
                fs.Write(data, 0, data.Length);
            }
            ShowInfo("录音已保存至" + path);
        }
        else
        {
            ShowInfo("请先停止录音！");
        }

    }

    /// <summary>
    /// 读取并播放
    /// </summary>
    public void ReadAndPlayer()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "example.wav");
        ShowInfo("开始读取录音并播放...");
        StartCoroutine(LoadAndPlayWav(filePath));
    }

    /// <summary>
    /// 读取录音文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    System.Collections.IEnumerator LoadAndPlayWav(string path)
    {
        if (File.Exists(path))
        {
            // 创建UnityWebRequest以读取文件
            using (var www = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    AudioClip clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(www);
                    aud.clip = clip;
                    aud.Play();
                }
                else
                {
                    Debug.LogError($"Error loading audio file: {www.error}");
                }
            }
        }
        else
        {
            Debug.LogError($"File not found at path: {path}");
        }
    }

    // 将录音转化为字节数组
    public static byte[] GetRealAudio(AudioClip recordedClip)
    {
        // 得到当前录音的位置（因为录音已经结束，也就是录音的长度）
        int position = Microphone.GetPosition(null);

        if (position <= 0 || position > recordedClip.samples)
        {
            position = recordedClip.samples;
        }
        // 录音的长度乘以它的通道
        float[] soundata = new float[position * recordedClip.channels];
        // 使用剪辑中的数据填充数组
        recordedClip.GetData(soundata, 0);
        // 创建一个音频文件（名字，长度，通道数，采样率）
        recordedClip = AudioClip.Create(recordedClip.name, position, recordedClip.channels, recordedClip.frequency, false);
        // recordedClip.SetData(soundata, 0);
        // short类型储存在最大值
        int rescaleFactor = 32767;
        // 创建一个空的字节数组，用于接收音频转化之后的信息
        byte[] outData = new byte[soundata.Length * 2];
        // 遍历所有采集到的音频信息
        for (int i = 0; i < soundata.Length; i++)
        {
            // short类型属于带符号的短整数类型，short类型占2字节(16位)内存空间，存储-32768 到 32767
            short temshort = (short)(soundata[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }
        return outData;
    }

    // 创建wav格式头文件
    private FileStream CreateEmpty(string filepath)
    {
        // 创建一个空的流文件
        FileStream fileStream = new FileStream(filepath, FileMode.Create);
        // 创建一个字符
        byte emptyByte = new byte();

        // 为wav文件头留出空间（在文件中写入44个空字符）
        for (int i = 0; i < 44; i++)
        {
            fileStream.WriteByte(emptyByte);
        }
        // 返回流文件
        return fileStream;
    }

    public static void WriteHeader(FileStream stream, AudioClip clip)
    {
        // 采样率
        int hz = clip.frequency;
        // 通道数
        int channels = clip.channels;
        // 长度
        int samples = clip.samples;

        stream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        // 从 riff 的 0 位置开始读取 4 个字节序列，然后将读出来的字节从 stream 的 position 位置开始存储在 stream 中
        stream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
        stream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        stream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        stream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        stream.Write(subChunk1, 0, 4);

        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        stream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        stream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        stream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        stream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        stream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        stream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        stream.Write(subChunk2, 0, 4);
    }
}