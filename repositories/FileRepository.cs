using System;
using System.IO;
using System.Threading.Tasks;

using ScheduleServer.Models;
using ScheduleServer.Libs;
using ScheduleServer.Configs;
using ScheduleServer.Exceptions;

namespace ScheduleServer.Repositories {
    public class FileRepository<K, V> : IAsyncRepository<K, V> {
        protected FileSystem fileSystem;
        protected FileRepositoryConfig config;
        protected ISerializable serializator;
        protected IGeneratable<object, string> nameGenerator;
        protected string root;

        public FileRepository(FileSystem fileSystem, FileRepositoryConfig config, ISerializable serializator, IGeneratable<object, string> nameGenerator) {
            this.fileSystem = fileSystem;
            this.config = config;
            this.serializator = serializator;
            this.nameGenerator = nameGenerator;
        }

        public void SetRootDirectory(string id) {
            root = config.GetDirectory(id);
        }

        public async Task<V> Get(K key) {
            try {
                var data = await fileSystem.GetFile(BuildPath(key));
                return serializator.Deserialize<V>(data);
            }
            catch (DirectoryNotFoundException) {
                throw new NotFoundException();
            }
            catch (FileNotFoundException) {
                throw new NotFoundException();
            }
        }

        public void Remove(K key) {
            fileSystem.DeleteFile(BuildPath(key));
        }

        public void RemoveAll() {
            fileSystem.ClearDirectory(root);
        }

        public void Add(K key, V value) {
            var data = serializator.Serialize(value);
            fileSystem.SaveFile(BuildPath(key), data);
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
        }

        private string BuildPath(object value) {
            return Path.Combine(root, nameGenerator.Generate(value));
        }
    }
}