using System.Text;
using System.Text.RegularExpressions;

namespace ScheduleServer.Libs {
    public class JsonNameGenerator : IGeneratable<object, string> {
        protected string extension;
        protected string separator;

        public JsonNameGenerator() {
            extension = "json";
            separator = ".";
        }

        public string Generate(object value) {
            var name = Build(Regex.Matches(value.ToString(), @"[\w\d]+"));

            return $"{name}{separator}{extension}";
        }

        protected string Build(MatchCollection items) {
            var builder = new StringBuilder();

            foreach (var item in items) {
                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}