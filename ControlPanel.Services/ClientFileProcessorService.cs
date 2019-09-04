using ControlPanel.Domain;
using ControlPanel.Domain.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControlPanel.Services
{
    public class ClientFileProcessorService : IClientFileProcessorService
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomEncryptionService _customEncryptionService;

        public ClientFileProcessorService(ICustomEncryptionService customEncryptionService, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _customEncryptionService = customEncryptionService;
        }

        public async Task<string> EncryptAndPostAsync(ZipArchive zipArchive, string fileName)
        {
            var message = string.Empty;
            try
            {
                var fileHierarchy = GetFileHierarchy(zipArchive);
                try
                {
                    var chiper = _customEncryptionService.EncryptStringToBytes(fileHierarchy);
                    try
                    {
                        var request = new RequestDto
                        {
                            FileName = fileName,
                            EncryptedHierarchy = chiper
                        };

                        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/FileInfo")
                        {
                            Content = new JsonContent(request),
                        };

                        var response = await _httpClient.SendAsync(httpRequest);

                        if (response.IsSuccessStatusCode)
                        {
                            message = await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            message = "Error occurred while contacting api.";
                        }
                    }
                    catch (Exception ex)
                    {
                        message = $"Error occurred while contacting api. {ex.Message}";
                    }
                }
                catch
                {
                    message = "Error occurred while encrypting file hierarchy.";
                }
            }
            catch
            {
                message = "Error occurred while generating file hierarchy.";
            }
            return message;
        }

        private string GetFileHierarchy(ZipArchive zipArchive)
        {
            ReadOnlyCollection<ZipArchiveEntry> innerFile = zipArchive.Entries;
            List<string> listOfFiles = innerFile.Select(x => x.FullName).ToList();
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (string file in listOfFiles)
            {
                CreatePath(nodeList, file);
            }
            return JsonConvert.SerializeObject(nodeList);
        }

        private void CreatePath(List<TreeNode> nodeList, string path)
        {
            TreeNode node = null;
            string folder = string.Empty;

            int p = path.IndexOf('/');

            if (p == -1)
            {
                folder = path;
                path = "";
            }
            else
            {
                folder = path.Substring(0, p);
                path = path.Substring(p + 1, path.Length - (p + 1));
            }

            node = null;

            foreach (TreeNode item in nodeList)
            {
                if (item.Name == folder)
                {
                    node = item;
                }
            }

            if (node == null)
            {
                node = new TreeNode(folder);
                nodeList.Add(node);
            }

            if (path != "")
            {
                if (node.Children == null)
                    node.Children = new List<TreeNode>();
                CreatePath(node.Children, path);
            }
        }
    }
}
