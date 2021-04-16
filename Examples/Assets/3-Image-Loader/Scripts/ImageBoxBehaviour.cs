#nullable enable
using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace ImageLoader.Scripts
{
    public class ImageBoxBehaviour : MonoBehaviour
    {
        [SerializeField] private UIDocument? rootDocument;

        private void Awake()
        {
            SetImages();
        }

        private void SetImages()
        {
            rootDocument!.rootVisualElement.Query(classes: "image-box").Children<Image>().ForEach(async i =>
            {
                var tex = await LoadImage("https://picsum.photos/200");
                return i.image = tex;
            });
        }

        private async UniTask<Texture> LoadImage(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();

            return request.result switch
            {
                UnityWebRequest.Result.Success => ((DownloadHandlerTexture) request.downloadHandler).texture,
                UnityWebRequest.Result.ConnectionError => throw new IOException(request.error),
                UnityWebRequest.Result.ProtocolError => throw new IOException(request.error),
                UnityWebRequest.Result.DataProcessingError => throw new IOException(request.error),
                UnityWebRequest.Result.InProgress => throw new InvalidOperationException("Web request result should not be in progress"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}