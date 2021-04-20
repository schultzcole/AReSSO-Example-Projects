#nullable enable
using System;
using System.IO;
using Cysharp.Threading.Tasks;
using ImageLoader.Scripts.State;
using ImageLoader.Scripts.State.Actions;
using Playdux.src.Store;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace ImageLoader.Scripts
{
    public class ImageRequestHandler : ISideEffector<ImageLoaderState>
    {
        public int Priority => 0;


        public bool PreEffect(DispatchedAction dispatchedAction, IStore<ImageLoaderState> store)
        {
            if (dispatchedAction.IsCanceled) return true;
            if (dispatchedAction.Action is not ImageRequestAction action) return true;

            var (slot, url) = action;
            LaunchRequest(slot, url, store);
            return true;
        }

        private static async void LaunchRequest(int slot, string url, IStore<ImageLoaderState> store)
        {
            Texture2D? texture = null;
            try { texture = await LoadRemoteTexture(url); }
            catch (IOException e) { Debug.LogException(e); }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }

            if (texture is null) return;

            store.Dispatch(new ImageResultAction(slot, texture));
        }

        private static async UniTask<Texture2D> LoadRemoteTexture(string url)
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

        public void PostEffect(DispatchedAction dispatchedAction, IStore<ImageLoaderState> store) { }
    }
}