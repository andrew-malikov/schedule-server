using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configuration {
    public class EFConfigProvider : ConfigurationProvider {
        public EFConfigProvider(Action<DbContextOptionsBuilder> optionsAction) {
            OptionsAction = optionsAction;
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load() {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);

            using (var context = new ConfigurationContext(builder.Options)) {
                context.Database.EnsureCreated();
                Console.WriteLine(context.Database.GetDbConnection());
                Data = GetDefaultValues();

                if (context.Values.Any()) Data = context.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        public override void Set(string key, string value) {
            Data[key] = value;

            using (var context = GetContext()) {
                var item = context.Values.Find(key);

                if (item is null) {
                    context.Values.Add(new ConfigurationValue() { Id = key, Value = value });
                }
                else item.Value = value;

                context.SaveChanges();
            }
        }

        protected ConfigurationContext GetContext() {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);

            return new ConfigurationContext(builder.Options);
        }

        private static IDictionary<string, string> GetDefaultValues() {
            return new Dictionary<string, string>();
        }
    }
}