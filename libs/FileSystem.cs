using System.IO;
using System.Threading.Tasks;

namespace ScheduleServer.Libs {
    public class FileSystem {
        public FileSystem() { }

        public async Task<string> GetFile(string path) {
            using (var file = File.OpenText(path)) {
                return await file.ReadToEndAsync();
            }
        }

        public void CreateFile(string path) {
            SaveFile(path, string.Empty);
        }

        public void SaveFile(string path, string content) {
            CreateDirectory(path);

            using (var file = File.CreateText(path)) {
                file.WriteAsync(content);
            }
        }

        public void DeleteFile(string path, string content) {
            File.Delete(path);
        }

        public void CreateDirectory(string path) {
            var directory = Path.GetDirectoryName(path);

            if (!ExistDirectory(directory))
                Directory.CreateDirectory(directory);
        }

        public void ClearDirectory(string path) {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.EnumerateFiles()) {
                file.Delete();
            }
            foreach (var subdirectory in directory.EnumerateDirectories()) {
                subdirectory.Delete(true);
            }
        }

        public bool ExistDirectory(string path) {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
                return true;

            return Directory.Exists(path);
        }

        public bool Exist(string path) {
            return File.Exists(path);
        }
    }
}