using ScheduleServer.Models;
using ScheduleServer.Libs;
using ScheduleServer.Configs;
using System.IO;
using System.Threading.Tasks;
using System;

namespace ScheduleServer.Repositories {
    public class FileRepository<K> : IAsyncRepository<K, string> {
        protected FileSystem fileSystem;
        protected FileRepositoryConfig config;
        protected string root;

        public FileRepository(FileSystem fileSystem, FileRepositoryConfig config) {
            this.fileSystem = fileSystem;
            this.config = config;
        }
        
        public void SetRootDirectory(string id) {
            root = config.GetDirectory(id);
        }

        public async Task<string> Get(K key) {
            return await fileSystem.GetFile(BuildPath(key.ToString()));
        }

        public void Remove(K key) {
            fileSystem.DeleteFile(BuildPath(key.ToString()));
        }

        public void RemoveAll() {
            fileSystem.ClearDirectory(root);
        }

        public void Add(K key, string value) {
            fileSystem.SaveFile(BuildPath(key.ToString()), value);
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
        }

        private string BuildPath(string path) {
            return Path.Combine(root, path);
        }
    }
}